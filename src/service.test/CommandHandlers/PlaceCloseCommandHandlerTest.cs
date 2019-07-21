using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.Newmoon.Places.Core;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceCloseCommandHandlerTest : IDisposable
    {
        private readonly string _defaultName = "Place";
        private readonly Address _defaultAddress = new Address("1", "st.", "akl", "ak", "nz","0920");
        
        [Fact]
        public async void Handle()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository<Guid, Place>>();

            var place = new Place(_defaultName, _defaultAddress);
            Place savedPlace = null;

            repositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Place>()))
                            .Callback<Place>(r => savedPlace = r);
            repositoryMock.Setup(repository => repository.FindByIdAsync(It.IsAny<Guid>()))
                            .Returns<Guid>(x => Task.FromResult(place));
            repositoryMock.Setup(repository => repository.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                            .Returns(Task.FromResult(true));

            var handler = new PlaceCloseCommandHandler(repositoryMock.Object);
            
            // Act
            var msg = new PlaceCloseCommand(Guid.NewGuid());
            var token = new CancellationToken();
            var result = await handler.Handle(msg, token);

            // Assert
            Assert.True(result);
            // Repository.FindById is called
            repositoryMock.Verify(p => p.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
            // Repository.Update is called
            repositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Place>()), Times.Once);
            // SaveEntitiesAsync is called
            repositoryMock.Verify(p => p.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
            // Verify Saved value
            Assert.NotNull(savedPlace);
            Assert.Equal(_defaultName, savedPlace.Name);
            Assert.Equal(_defaultAddress, savedPlace.Address);
            Assert.Equal(PlaceState.TemporaryClosed, savedPlace.State);

            repositoryMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async void NotExisted()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository<Guid, Place>>();
            
            repositoryMock.Setup(repository => repository.FindByIdAsync(It.IsAny<Guid>()))
                .Returns<Guid>(x => Task.FromResult<Place>(null));

            var handler = new PlaceCloseCommandHandler(repositoryMock.Object);
            
            // Act
            var msg = new PlaceCloseCommand(Guid.NewGuid());
            var token = new CancellationToken();
            var result = await handler.Handle(msg, token);

            // Assert
            Assert.False(result);
            // Repository.FindById is called
            repositoryMock.Verify(p => p.FindByIdAsync(It.IsAny<Guid>()), Times.Once);

            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void EmptyCtor()
        {
            Assert.Throws<ArgumentNullException>(() => new PlaceCloseCommandHandler(null));
        }
        
        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
