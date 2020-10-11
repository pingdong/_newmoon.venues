using PingDong.CQRS;
using PingDong.CQRS.Core.Testing;
using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Newmoon.Venues.Testings;
using PingDong.Validations;
using System;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueTest
    {
        #region State

        #region Occupy

        [Theory, MockInjection]
        public void Status_Occupy(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.ClearDomainEvents();

            // ACT
            venue.Occupy();

            // ASSERT
            Assert.Equal(1, venue.HasDomainEvent(typeof(VenueOccupiedDomainEvent)));
            Assert.Equal(VenueStatus.Occupied, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Occupy_UnavailableVenue(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.Close();
            venue.ClearDomainEvents();
            
            // ACT
            Assert.Throws<EntityException>(venue.Occupy);
            
            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Unavailable, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Occupy_Occupied(
            Venue venue)
        {
            // ASSERT
            venue.Open();
            venue.Occupy();
            venue.ClearDomainEvents();

            // AcT
            Assert.Throws<EntityException>(venue.Occupy);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Occupied, venue.Status);
        }

        #endregion

        #region Vacate

        [Theory, MockInjection]
        public void Status_Vacate(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.Occupy();
            venue.ClearDomainEvents();

            // ACT
            venue.Vacate();

            // ASSERT
            Assert.Equal(1, venue.HasDomainEvent(typeof(VenueVacatedDomainEvent)));
            Assert.Equal(VenueStatus.Vacancy, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Vacate_UnavailableVenue(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.Close();
            venue.ClearDomainEvents();

            // ACT
            Assert.Throws<EntityException>(venue.Vacate);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Unavailable, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Vacate_Vacated(
            Venue venue)
        {
            // ASSERT
            venue.Open();
            venue.Occupy();
            venue.Vacate();
            venue.ClearDomainEvents();

            // AcT
            Assert.Throws<EntityException>(venue.Vacate);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Vacancy, venue.Status);
        }

        #endregion

        #region Close

        [Theory, MockInjection]
        public void Status_Close(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.ClearDomainEvents();

            // ACT
            venue.Close();

            // ASSERT
            Assert.Equal(1, venue.HasDomainEvent(typeof(VenueClosedDomainEvent)));
            Assert.Equal(VenueStatus.Unavailable, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Close_UnavailableVenue(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.Close();
            venue.ClearDomainEvents();

            // ACT
            Assert.Throws<EntityException>(venue.Close);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Unavailable, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Close_Occupied(
            Venue venue)
        {
            // ASSERT
            venue.Open();
            venue.Occupy();
            venue.ClearDomainEvents();

            // AcT
            Assert.Throws<EntityException>(venue.Close);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Occupied, venue.Status);
        }

        #endregion

        #region Open

        [Theory, MockInjection]
        public void Status_Open(
            Venue venue)
        {
            // ACT
            venue.Open();

            // ASSERT
            Assert.Equal(1, venue.HasDomainEvent(typeof(VenueOpenedDomainEvent)));
            Assert.Equal(VenueStatus.Vacancy, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Open_OpenedVenue(
            Venue venue)
        {
            // ARRANGE
            venue.Open();
            venue.ClearDomainEvents();

            // ACT
            Assert.Throws<EntityException>(venue.Open);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Vacancy, venue.Status);
        }

        [Theory, MockInjection]
        public void Status_Open_Occupied(
            Venue venue)
        {
            // ASSERT
            venue.Open();
            venue.Occupy();
            venue.ClearDomainEvents();

            // AcT
            Assert.Throws<EntityException>(venue.Open);

            // ASSERT
            Assert.True(!venue.HasDomainEvents());
            Assert.Equal(VenueStatus.Occupied, venue.Status);
        }

        #endregion

        #endregion

        #region Properties

        [Theory, MockInjection]
        public void Properties(
            Address address
            , string name)
        {
            var venue = new Venue(name, address);

            Assert.Equal(name, venue.Name);
            Assert.Equal(address, venue.Address);
        }
        
        [Theory, MockInjection]
        public void Properties_Update(
            Address address
            , Address newAddress
            , string name
            , string newName)
        {
            var venue = new Venue(name, address);

            Assert.Equal(name, venue.Name);
            Assert.Equal(address, venue.Address);

            Assert.NotEqual(name, newName);
            Assert.NotEqual(address, newAddress);

            venue.Update(newName, newAddress);
            Assert.Equal(newName, venue.Name);
            Assert.Equal(newAddress, venue.Address);
        }

        #endregion

        #region Validation

        [Theory, MockInjection]
        public void Validation_Constructor(
            Address address
            , string venueName)
        {
            Assert.Throws<ArgumentNullException>(() => new Venue(null));
            Assert.Throws<ArgumentNullException>(() => new Venue(string.Empty));
            Assert.Throws<ArgumentNullException>(() => new Venue(" "));

            Assert.Throws<ArgumentNullException>(() => new Venue(null, address));
            Assert.Throws<ArgumentNullException>(() => new Venue(string.Empty, address));
            Assert.Throws<ArgumentNullException>(() => new Venue(" ", address));

            Assert.Throws<ValidationFailureException>(() => new Venue(venueName, new Address()));
        }

        [Theory, MockInjection]
        public void Validation_Update(
            Address address
            , Venue venue
            , string venueName)
        {
            Assert.Throws<ArgumentNullException>(() => venue.Update(null, address));
            Assert.Throws<ArgumentNullException>(() => venue.Update(string.Empty, address));
            Assert.Throws<ArgumentNullException>(() => venue.Update(" ", address));
            
            Assert.Throws<ValidationFailureException>(() => venue.Update(venueName, new Address()));
        }

        #endregion
    }
}
