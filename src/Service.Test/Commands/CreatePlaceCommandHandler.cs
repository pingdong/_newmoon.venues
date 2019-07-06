using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.Newmoon.Places.Core;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommandHandlerTest : IDisposable
    {
        private readonly string _defaultName = "Place";
        private readonly Address _defaultAddress = new Address("1", "st.", "akl", "ak", "nz","0920");


        [Fact]
        public async void Handle()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository<Guid, Place>>();

            Place savedPlace = null;

            repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Place>()))
                            .Returns<Place>(x => Task.FromResult(x))
                            .Callback<Place>(r => savedPlace = r);
            repositoryMock.Setup(repository => repository.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                            .Returns(Task.FromResult(true));

            var handler = new CreatePlaceCommandHandler(repositoryMock.Object);
            
            // Act
            var msg = new CreatePlaceCommand(_defaultName, _defaultAddress);
            var token = new CancellationToken();
            var result = await handler.Handle(msg, token);

            // Assert
            Assert.True(result);
            // Repository.Add is called
            repositoryMock.Verify(p => p.AddAsync(It.IsAny<Place>()), Times.Once);
            // SaveEntitiesAsync is called
            repositoryMock.Verify(p => p.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
            // Verify Saved value
            Assert.NotNull(savedPlace);
            Assert.Equal(_defaultName, savedPlace.Name);
            Assert.Equal(_defaultAddress, savedPlace.Address);

            repositoryMock.VerifyNoOtherCalls();
        }


        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
