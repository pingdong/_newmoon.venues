using AutoFixture.Xunit2;
using Moq;
using PingDong.CQRS.Infrastructure;
using PingDong.Newmoon.Venues.Testings;
using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class CommandHandlerTest
    {
        #region GetVenue

        [Theory, ServiceInjection]
        public void GetVenue_ShouldThrowException_WhenVenueIdIsEmpty(
            [Frozen] IRequestMetadata metadata
            , [Frozen]CommandHandler handler)
        {
            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.GetVenueAsync(Guid.Empty, metadata, true));
        }

        [Theory, ServiceInjection]
        public void GetVenue_ShouldThrowException_WhenMetaDataIsNull(
            Guid venueId
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.GetVenueAsync(venueId, null, true));
        }

        [Theory, ServiceInjection]
        public void GetVenue_ShouldThrowException_WhenThrowExceptionIfVenueNotExisted(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , [Frozen] IRequestMetadata metadata
            , Guid venueId
            , [Frozen] CommandHandler handler)
        {
            // ARRANGE
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .ThrowsAsync(new NullReferenceException());

            // ACT
            Assert.ThrowsAsync<NullReferenceException>(() => handler.GetVenueAsync(venueId, metadata, true));
        }

        [Theory, ServiceInjection]
        public async void GetVenue_ShouldReturnNull_WhenThrowExceptionIfVenueNotExisted(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , [Frozen] IRequestMetadata metadata
            , Guid venueId
            , [Frozen] CommandHandler handler)
        {
            // ARRANGE
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((Venue) null);

            // ACT
            var venue = await handler.GetVenueAsync(venueId, metadata, false);

            // ASSERT
            Assert.Null(venue);
        }

        [Theory, ServiceInjection]
        public async void GetVenue_ShouldReturn_WithMetadata(
            [Frozen] Mock<IRepository<Guid, Venue>> repository
            , [Frozen] Mock<IRequestMetadata> metadata
            , Guid venueId
            , Venue venue
            , [Frozen] CommandHandler handler)
        {
            // ARRANGE
            repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                      .ReturnsAsync(venue);
            metadata.Setup(m => m.TenantId)
                    .Returns(Guid.NewGuid().ToString());
            metadata.Setup(m => m.CorrelationId)
                    .Returns(Guid.NewGuid().ToString());

            // ACT
            var result = await handler.GetVenueAsync(venueId, metadata.Object, false);

            // ASSERT
            Assert.Equal(result, venue);

            metadata.Verify(m => m.TenantId, Times.Once());
            metadata.Verify(m => m.CorrelationId, Times.Once());
        }

        #endregion

        #region Create Venue

        [Theory, ServiceInjection]
        public void CreateVenue_ShouldThrowException_WhenVenueNameIsNull(
            [Frozen] IRequestMetadata metadata
            , Address address
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => handler.CreateVenue(null, address, metadata));
        }

        [Theory, ServiceInjection]
        public void CreateVenue_ShouldThrowException_WhenVenueNameIsWhitespace(
            [Frozen] IRequestMetadata metadata
            , Address address
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => handler.CreateVenue(" ", address, metadata));
        }

        [Theory, ServiceInjection]
        public void CreateVenue_ShouldThrowException_WhenVenueNameIsEmpty(
            [Frozen] IRequestMetadata metadata
            , Address address
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => handler.CreateVenue(string.Empty, address, metadata));
        }

        [Theory, ServiceInjection]
        public void CreateVenue_ShouldThrowException_WhenAddressIsNull(
            [Frozen] IRequestMetadata metadata
            , string venueName
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => handler.CreateVenue(venueName, null, metadata));
        }

        [Theory, ServiceInjection]
        public void CreateVenue_ShouldThrowException_WhenMetadataIsNull(
            string venueName
            , Address address
            , [Frozen] CommandHandler handler)
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => handler.CreateVenue(venueName, address, null));
        }

        [Theory, ServiceInjection]
        public async void CreateVenue_ShouldReturn_WithMetadata(
            Mock<IRequestMetadata> metadata
            , Address address
            , string venueName
            , [Frozen] CommandHandler handler)
        {
            // ARRANGE
            metadata.Setup(m => m.TenantId)
                    .Returns(Guid.NewGuid().ToString());
            metadata.Setup(m => m.CorrelationId)
                    .Returns(Guid.NewGuid().ToString());

            // ACT
            var result = handler.CreateVenue(venueName, address, metadata.Object);

            // ASSERT
            Assert.Equal(venueName, result.Name);
            Assert.Equal(address, result.Address);

            metadata.Verify(m => m.TenantId, Times.Once());
            metadata.Verify(m => m.CorrelationId, Times.Once());
        }

        #endregion
    }
}
