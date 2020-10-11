using PingDong.Messages;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class DumpPublisher : IPublisher
    {
        public Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            return Task.CompletedTask;
        }
    }
}
