using AutoFixture.Xunit2;
using MediatR;
using Moq;
using PingDong.Messages;
using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Newmoon.Venues.Testings;
using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.DomainEvents
{
    public class VenueClosedDomainEventHandlerTest
    {
        [Theory, ServiceInjection]
        public async void Command_Should_BeHandled(
            [Frozen] Mock<IMessagePublisher> publisher
            , [Frozen] Mock<IMediator> mediator
            , [Frozen] VenueClosedDomainEvent evt
            , VenueClosedDomainEventHandler handler
            )
        {
            // ARRANGE
            publisher.Setup(r => r.PublishAsync(It.IsAny<IntegrationEvent>()));
            
            // ACT
            await handler.Handle(evt);

            // ASSERT
            // Integration Event Published
            publisher.Verify(p => p.PublishAsync(It.IsAny<IntegrationEvent>()));

            mediator.VerifyNoOtherCalls();
            publisher.VerifyNoOtherCalls();
        }

        [Theory, ServiceInjection]
        public async void Null_Should_ThrowException(
            [Frozen] Mock<IPublisher> publisher
            , [Frozen] Mock<IMediator> mediator
            , VenueClosedDomainEventHandler handler
        )
        {
            // ACT
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => handler.Handle(null));

            // ASSERT
            mediator.VerifyNoOtherCalls();
            publisher.VerifyNoOtherCalls();
        }
    }
}
