using System;
using System.Threading;
using MediatR;
using Moq;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Service.Commands;
using PingDong.Newmoon.Places.Service.IntegrationEvents;
using Xunit;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class EventCanceledIntegrationEventHandlerTest : IDisposable
    {
        [Fact]
        public async void Handle()
        {
            // Arrange
            var mediator = new Mock<IMediator>();

            IdentifiedCommand<Guid, bool, Command<bool>> command = null;

            mediator.Setup(svc => svc.Send(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<bool>, CancellationToken>((cmd, token) =>
                {
                    command = cmd as IdentifiedCommand<Guid, bool, Command<bool>>;
                })
                .ReturnsAsync(true)
                .Verifiable();

            var handler = new EventCanceledIntegrationEventHandler(mediator.Object);
            
            // Act
            var integrationEvent = new EventCanceledIntegrationEvent(Guid.NewGuid(), Guid.NewGuid())
            {
                CorrelationId = Guid.NewGuid().ToString(),
                TenantId = "TestTenant",
                RequestId = Guid.NewGuid().ToString()
            };
            await handler.Handle(integrationEvent);

            // Assert
            // Integration Event Published
            mediator.Verify(p => p.Send(It.IsAny<IRequest<bool>>(), default));
            
            Assert.NotNull(command);
            Assert.Equal(command.Id.ToString(), integrationEvent.RequestId);

            var calledCommand = command.Command as PlaceFreeCommand;

            Assert.NotNull(calledCommand);
            Assert.Equal(calledCommand.Id, integrationEvent.PlaceId);
            Assert.Equal(calledCommand.CorrelationId, integrationEvent.CorrelationId);
            Assert.Equal(calledCommand.TenantId, integrationEvent.TenantId);

            mediator.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Null()
        {
            // Arrange
            var mediator = new Mock<IMediator>();

            var handler = new EventCanceledIntegrationEventHandler(mediator.Object);
            
            // Act
            var integrationEvent = new EventCanceledIntegrationEvent(Guid.NewGuid(), Guid.Empty)
            {
                CorrelationId = Guid.NewGuid().ToString(),
                TenantId = "TestTenant",
                RequestId = Guid.NewGuid().ToString()
            };
            await handler.Handle(integrationEvent);

            // Assert
            mediator.VerifyNoOtherCalls();
        }

        [Fact]
        public async void EmptyPlaceId()
        {
            // Arrange
            var mediator = new Mock<IMediator>();

            var handler = new EventCanceledIntegrationEventHandler(mediator.Object);
            
            // Act
            await handler.Handle(null);

            // Assert
            mediator.VerifyNoOtherCalls();
        }

        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
