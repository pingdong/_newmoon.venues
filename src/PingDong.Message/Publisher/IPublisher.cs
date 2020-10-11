using System.Threading.Tasks;

namespace PingDong.Messages
{
    public interface IPublisher
    {
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
    }
}
