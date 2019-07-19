using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Azure;
using PingDong.EventBus.Core;

namespace PingDong.Newmoon.Places.Service
{
    public class Registrar
    {
        public virtual void Register(IServiceCollection services)
        {
            // Idempotency: Register RequestManager
            services.AddScoped(typeof(IRequestManager<>), typeof(RequestManager<>));
            
            // MediatR: Register all behaviors
            services.AddMediatR(
                //     From CleanArchitect.Service
                typeof(IRequestManager<>).Assembly,
                //     From CleanCurrent Assembly
                Assembly.GetExecutingAssembly()
            );
            
            // ServiceBus
            //    Register Publisher
            services.AddScoped<IEventBusPublisher, TopicPublisher>();
            //services.AddScoped<IEventBusPublisher, QueuePublisher>(x => 
            //    new QueuePublisher(x.GetRequiredService<IConfiguration>(), ReceiveMode.PeekLock)
            //);
            //    Register Handle Dispatcher
            var subscriptions = CreateSubscriptionManager();
            // TODO: Register Dynamic Type Subscriber
            services.AddScoped<ISubscriptionsManager, SubscriptionsManager>(x => subscriptions);
            services.AddScoped<IMessageDispatcher<Message>, ServiceBusMessageDispatcher>(
                serviceProvider => new ServiceBusMessageDispatcher(
                    serviceProvider
                    ,serviceProvider.GetRequiredService<ILogger<ServiceBusMessageDispatcher>>()
                    ,subscriptions
                    ,"IntegrationEvent"
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
        }

        private SubscriptionsManager CreateSubscriptionManager()
        {
            const string EventTypeSuffix = "IntegrationEvent";
            const string EventHandlerSuffix = "IntegrationEventHandler";

            var subscriptions = new SubscriptionsManager();

            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();
            var eventTypes = types.Where(t => t.Name.EndsWith(EventTypeSuffix)).ToList();
            var eventHandlers = types.Where(t => t.Name.EndsWith(EventHandlerSuffix)).ToList();

            // Skipping to look for handlers for all outbound integration event
            foreach (var type in eventTypes)
            {
                var handler = eventHandlers.FirstOrDefault(t => t.Name.StartsWith(type.Name));
                if (handler != null)
                    subscriptions.AddSubscriber(type, handler);
            }

            return subscriptions;
        }
    }
}
