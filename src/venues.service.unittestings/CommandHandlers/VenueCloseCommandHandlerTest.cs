using AutoFixture.Xunit2;
using Moq;
using PingDong.CQRS.Infrastructure;
using PingDong.Newmoon.Venues.Testings;
using System;
using System.Threading;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommandHandlerTest
    {
        [Theory, ServiceMockInjectionAttribute]
        public async void Handle_Should_Persist(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , [Frozen] Venue venue
            , [Frozen] VenueCloseCommand command
            , VenueCloseCommandHandler handler)
        {
            // ARRANGE
            venue.Open();

            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .ReturnsAsync(venue);
            repository.Setup(r => r.UpdateAsync(It.IsAny<Venue>()));
            repository.Setup(r => r.UnitOfWork.SaveAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);

            // ACT
            var result = await handler.Handle(command);

            // Arrange
            Assert.True(result);
            // Repository.FindById is called
            repository.Verify(p => p.FindByIdAsync(command.Id, true), Times.Once);
            // Repository.Update is called
            repository.Verify(p => p.UpdateAsync(venue), Times.Once);
            // SaveEntitiesAsync is called
            repository.Verify(p => p.UnitOfWork.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
            
            repository.VerifyNoOtherCalls();
        }
        
        [Theory, ServiceMockInjectionAttribute]
        public async void CloseNotExisted_Should_ThrowException(
            [Frozen]Mock<IRepository<Guid, Venue>> repository
            , VenueCloseCommand command
            , VenueCloseCommandHandler handler
            )
        {
            // ARRANGE   
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .Throws<NotFoundException>();
            
            // ACT
            await Assert.ThrowsAnyAsync<NotFoundException>(() => handler.Handle(command));

            // ASSERT
            repository.Verify(p => p.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public void EmptyCtor_Should_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new VenueCloseCommandHandler(null));
        }
    }
}
