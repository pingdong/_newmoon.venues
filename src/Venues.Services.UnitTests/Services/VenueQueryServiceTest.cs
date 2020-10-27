using AutoFixture.Xunit2;
using Moq;
using PingDong.DDD.Infrastructure;
using PingDong.Newmoon.Venues.Tests;
using System;
using System.Linq;
using Xunit;

namespace PingDong.Newmoon.Venues.Services
{
    public class VenueQueryServiceTest
    {
        #region GetAll

        [Theory, ServiceInjection]
        public async void GetAllSync_ShouldCallRepository(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , VenueQueryService svc)
        {
            // ARRANGE
            repository.Setup(r => r.ListAsync())
                      .ReturnsAsync(new [] {new Venue("Test")});

            // ACT
            var venues = await svc.GetAllAsync();

            // ASSERT
            Assert.Single(venues);
            Assert.Equal("Test", venues.First().Name);
            Assert.Null(venues.First().Address);
        }

        #endregion

        #region GetById

        [Theory, ServiceInjection]
        public async void GetById_ShouldThrowException_WhenVenueIdIsEmpty(
            VenueQueryService svc)
        {
            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => svc.GetByIdAsync(Guid.Empty));
        }

        [Theory, ServiceInjection]
        public async void GetById_ShouldReturnVenue_Found(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , Guid venueId
            , VenueQueryService svc)
        {
            // ARRANGE
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .ReturnsAsync(new Venue("Test"));

            // ACT
            var venue = await svc.GetByIdAsync(venueId);

            // ARRANGE
            Assert.Equal("Test", venue.Name);
            Assert.Null(venue.Address);
        }

        [Theory, ServiceInjection]
        public async void GetById_ShouldReturnVenue_NotFound(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , Guid venueId
            , VenueQueryService svc)
        {
            // ARRANGE
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .ReturnsAsync((Venue)null);

            // ACT
            var venue = await svc.GetByIdAsync(venueId);

            // ARRANGE
            Assert.Null(venue);
        }

        #endregion
    }
}
