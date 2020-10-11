using System.Threading.Tasks;

namespace PingDong.Messages
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
