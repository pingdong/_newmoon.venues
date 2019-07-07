using System;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceTest
    {
        #region Occupied

        #region Init

        [Fact]
        public void Status_AfterCreated()
        {
            var place = CreateDefaultPlace();

            Assert.False(place.IsOccupied);
        }

        #endregion

        #region Engage

        [Fact]
        public void Engage()
        {
            var place = CreateDefaultPlace();

            Assert.False(place.IsOccupied);
            place.Engage();
            Assert.True(place.HasDomainEvent(typeof(PlaceEngagedDomainEvent)));
            Assert.True(place.IsOccupied);
        }

        [Fact]
        public void Engage_Occupied()
        {
            var place = CreateDefaultPlace();

            place.Engage();
            Assert.True(place.HasDomainEvent(typeof(PlaceEngagedDomainEvent)));
            Assert.True(place.IsOccupied);
            
            Assert.Throws<DomainException>(() => place.Engage());
            Assert.True(place.HasDomainEvent(typeof(PlaceEngagedDomainEvent)));
        }

        #endregion

        #region Disengage

        [Fact]
        public void Disengage()
        {
            var place = CreateDefaultPlace();

            place.Engage();
            Assert.True(place.HasDomainEvent(typeof(PlaceEngagedDomainEvent)));
            Assert.True(place.IsOccupied);

            place.Disengage();
            Assert.True(place.HasDomainEvent(typeof(PlaceDisengagedDomainEvent)));
            Assert.False(place.IsOccupied);

            Assert.True(place.HasDomainEvents(2));
            Assert.True(place.HasDomainEvents( new[]{ typeof(PlaceEngagedDomainEvent), typeof(PlaceDisengagedDomainEvent) } ));
        }

        [Fact]
        public void Disengage_Unoccupied()
        {
            var place = CreateDefaultPlace();

            Assert.False(place.IsOccupied);
            Assert.Throws<DomainException>(() => place.Disengage());
            Assert.True(place.HasNoDomainEvent());
        }

        #endregion

        #endregion

        #region Properties

        [Fact]
        public void Properties()
        {
            var address = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
            var name = "Test";

            var place = new Place(name, address);

            Assert.Equal(name, place.Name);
            Assert.Equal(address, place.Address);
            Assert.False(place.IsOccupied);
        }
        
        [Fact]
        public void Properties_Update()
        {
            var address = CreateDefaultAddress();
            var name = "Test";

            var place = new Place(name, address);
            Assert.Equal(name, place.Name);
            Assert.Equal(address, place.Address);

            var newName = "New Test";
            var newAddress = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
            Assert.NotEqual(name, newName);
            Assert.NotEqual(address, newAddress);

            place.Update(newName, newAddress);
            Assert.Equal(newName, place.Name);
            Assert.Equal(newAddress, place.Address);
            Assert.False(place.IsOccupied);
        }

        #endregion

        #region Validation

        [Fact]
        public void Validation_Constructor()
        {
            Assert.Throws<ArgumentNullException>(() => new Place(null, CreateDefaultAddress()));
            Assert.Throws<ArgumentNullException>(() => new Place(string.Empty, CreateDefaultAddress()));
            Assert.Throws<ArgumentNullException>(() => new Place(" ", CreateDefaultAddress()));
            
            Assert.Throws<ArgumentNullException>(() => new Place("Test", null));
        }

        [Fact]
        public void Validation_Update()
        {
            var place = CreateDefaultPlace();
            
            Assert.Throws<ArgumentNullException>(() => place.Update(null, CreateDefaultAddress()));
            Assert.Throws<ArgumentNullException>(() => place.Update(string.Empty, CreateDefaultAddress()));
            Assert.Throws<ArgumentNullException>(() => place.Update(" ", CreateDefaultAddress()));
            
            Assert.Throws<ArgumentNullException>(() => place.Update("Test", null));
        }

        #endregion

        #region Helper

        private Place CreateDefaultPlace()
        {
            return new Place("Default", CreateDefaultAddress());
        }

        private Address CreateDefaultAddress()
        {
            return new Address("1", "Queen St.", "Auckland", "Auckland", "New Zealand", "0926");
        }

        #endregion
    }
}
