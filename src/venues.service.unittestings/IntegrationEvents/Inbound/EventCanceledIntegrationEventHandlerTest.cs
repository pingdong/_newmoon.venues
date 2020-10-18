using AutoFixture.Xunit2;
using MediatR;
using Moq;
using PingDong.CQRS.Services;
using PingDong.Newmoon.Venues.Testings;
using System;
using System.Threading;
using Microsoft.Extensions.Options;
using PingDong.Newmoon.Venues.Settings;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class EventCanceledIntegrationEventHandlerTest
    {
        [Theory, ServiceInjection]
        public async void Handle_Should_Success(
            [Frozen] Mock<IMediator> mediator
            , [Frozen] EventCanceledIntegrationEvent @event
            , EventCanceledIntegrationEventHandler handler)
        {
            // ARRANGE
            mediator.Setup(r => r.Send(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()));

            // ACT
             await handler.Handle(@event);

            // ARRANGE
            mediator.Verify(p => p.Send(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()), Times.Once);

            mediator.VerifyNoOtherCalls();
        }

        [Theory, ServiceInjection]
        public async void Null_Should_ThrowException(
            [Frozen] Mock<IMediator> mediator
            , EventCanceledIntegrationEventHandler handler
            )
        {
            // ACT
            await Assert.ThrowsAnyAsync<IntegrationEventException>(() => handler.Handle(null));

            // ASSERT
            mediator.VerifyNoOtherCalls();
        }

        [Theory, ServiceInjection]
        public async void Empty_Should_ThrowException(
            [Frozen] Mock<IMediator> mediator
            , EventCanceledIntegrationEventHandler handler
        )
        {
            // ARRANGE
            var evt = new EventCanceledIntegrationEvent(Guid.NewGuid(), Guid.Empty);
            
            // ACT
            await Assert.ThrowsAnyAsync<IntegrationEventException>(() => handler.Handle(evt));

            // ASSERT
            mediator.VerifyNoOtherCalls();
        }

        [Theory, ServiceInjection]
        public void EmptyCtor_Should_ThrowException(
            IOptionsMonitor<AppSettings> settings)
        {
            Assert.Throws<ArgumentNullException>(() => new EventCanceledIntegrationEventHandler(null, settings));
        }
    }
}
