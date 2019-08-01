using System;
using System.Threading;
using MediatR;
using Moq;
using PingDong.CleanArchitect.Core;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;
using Xunit;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class PlaceFreedDomainEventHandlerTest : IDisposable
    {
        [Fact]
        public async void Handle()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var eventBus = new Mock<IEventBusPublisher>();

            object @event = null;

            eventBus.Setup(svc => svc.PublishAsync(It.IsAny<IntegrationEvent>()))
                .Callback<IntegrationEvent>(evt => @event = evt);

            var handler = new PlaceFreedDomainEventHandler(eventBus.Object, mediator.Object);
            
            // Act
            var domainEvent = new PlaceFreedDomainEvent(Guid.NewGuid(), "Test");
            await handler.Handle(domainEvent, CancellationToken.None);

            // Assert
            // Integration Event Published
            eventBus.Verify(p => p.PublishAsync(It.IsAny<IntegrationEvent>()));

            var calledEvent = @event as PlaceFreedIntegrationEvent;
            Assert.NotNull(calledEvent);
            Assert.Equal(calledEvent.PlaceId, domainEvent.PlaceId);
            Assert.Equal(calledEvent.PlaceName, domainEvent.PlaceName);

            mediator.VerifyNoOtherCalls();
            eventBus.VerifyNoOtherCalls();
        }


        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
