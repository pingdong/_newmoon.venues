using System.Reflection;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Azure;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;

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
            var subscriptions = new SubscriptionsManager();
            subscriptions.RegisterIntegrationEvents();
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
    }
}
