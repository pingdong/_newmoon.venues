using System.Linq;
using System.Reflection;
using PingDong.EventBus.Core;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    internal static class SubscriptionManagerExtensions
    {
        private const string EventTypeSuffix = "IntegrationEvent";
        private const string EventHandlerSuffix = "IntegrationEventHandler";

        public static void RegisterIntegrationEvents(this ISubscriptionsManager subscriptions)
        {
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
        }
    }
}
