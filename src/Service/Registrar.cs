using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Azure;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // Idempotency: Register RequestManager
            services.AddScoped(typeof(IRequestManager<>), typeof(RequestManager<>));
            
            // MediatR
            services.AddMediatR(
                //     From CleanArchitect.Service
                typeof(IRequestManager<>).Assembly,
                //     From CleanCurrent Assembly
                Assembly.GetExecutingAssembly()
            );

            // FluentValidation
            services.AddValidatorsFromAssemblies(new [] {
                //    From this assembly
                Assembly.GetExecutingAssembly()
            });
            
            // ServiceBus
            //    Register Publisher
            services.AddScoped<IEventBusPublisher, TopicPublisher>();
            //services.AddScoped<IEventBusPublisher, QueuePublisher>(x => 
            //    new QueuePublisher(x.GetRequiredService<IConfiguration>(), ReceiveMode.PeekLock)
            //);
            //    Register Handle Dispatcher
            var subscriptions = CreateSubscriptionManager();
            services.AddScoped<ISubscriptionsManager, SubscriptionsManager>(x => subscriptions);
            services.AddScoped<IMessageDispatcher<Message>, ServiceBusMessageDispatcher>(
                serviceProvider => new ServiceBusMessageDispatcher(
                    serviceProvider
                    , serviceProvider.GetRequiredService<ILogger<ServiceBusMessageDispatcher>>()
                    , subscriptions
                    , EventTypeSuffix
                )
            );
            //   Register Integration Event Handler
            services.Scan(scan => scan
                .FromCallingAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(IDynamicIntegrationEventHandler)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IIntegrationEventHandler<>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
            );

            // Services
            services.AddScoped<ITenantValidator, TenantValidator>();
        }
        
        private const string EventTypeSuffix = "IntegrationEvent";
        private const string EventHandlerSuffix = "IntegrationEventHandler";
        private const string DynamicEventHandlerSuffix = "DynamicIntegrationEventHandler";

        private SubscriptionsManager CreateSubscriptionManager()
        {

            var subscriptions = new SubscriptionsManager();

            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            // Fixed Type
            //      Naming:   Type: {Message.Label}{EventTypeSuffix}
            //             Handler: {Message.Label}{EventHandlerSuffix}
            var eventTypes = types.Where(t => t.Name.EndsWith(EventTypeSuffix)).ToList();
            var eventHandlers = types.Where(t => t.Name.EndsWith(EventHandlerSuffix)).ToList();
            foreach (var type in eventTypes)
            {
                var handler = eventHandlers.FirstOrDefault(t => t.Name.StartsWith(type.Name));
                // Skipping to look for handlers for all outbound integration event
                if (handler != null)
                    subscriptions.AddSubscriber(type, handler);
            }

            // Dynamic Type
            //      Naming:
            //             Handler: {Message.Label}{DynamicEventHandlerSuffix}
            var dynamicEventHandlers = types.Where(t => t.Name.EndsWith(DynamicEventHandlerSuffix)).ToList();
            foreach (var handler in dynamicEventHandlers)
            {
                var name = handler.Name;
                var dynamicEventName = name.Remove(name.IndexOf(DynamicEventHandlerSuffix, StringComparison.OrdinalIgnoreCase));
                subscriptions.AddSubscriber(dynamicEventName, handler);
            }

            return subscriptions;
        }
    }
}
