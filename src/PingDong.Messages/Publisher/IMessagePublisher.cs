using System.Threading.Tasks;

namespace PingDong.Messages
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
    }
}
