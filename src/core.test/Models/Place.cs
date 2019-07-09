using System;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceTest
    {
        #region State

        #region Occupy

        [Fact]
        public void State_Occupy()
        {
            var place = CreateDefaultPlace();

            place.Occupy();

            Assert.True(place.HasDomainEvent(typeof(PlaceOccupiedDomainEvent)));
            Assert.Equal(PlaceState.Occupied, place.State);
        }

        [Fact]
        public void State_Occupy_ClosedPlace()
        {
            var place = CreateDefaultPlace();
            place.Close();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Occupy());

            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.TemporaryClosed, place.State);
        }

        [Fact]
        public void State_Occupy_Occupied()
        {
            var place = CreateDefaultPlace();
            place.Occupy();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Occupy());
            
            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Occupied, place.State);
        }

        #endregion

        #region Free

        [Fact]
        public void State_Free()
        {
            var place = CreateDefaultPlace();

            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Free, place.State);
        }

        [Fact]
        public void State_Free_Freed()
        {
            var place = CreateDefaultPlace();

            Assert.Throws<DomainException>(() => place.Free());

            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Free, place.State);
        }

        [Fact]
        public void State_Free_TemporaryClosed()
        {
            var place = CreateDefaultPlace();
            place.Close();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Free());
            
            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.TemporaryClosed, place.State);
        }

        #endregion

        #region Close

        [Fact]
        public void State_Close()
        {
            var place = CreateDefaultPlace();

            place.Close();

            Assert.True(place.HasDomainEvent(typeof(PlaceTemporaryClosedDomainEvent)));
            Assert.Equal(PlaceState.TemporaryClosed, place.State);
        }

        [Fact]
        public void State_Close_Closed()
        {
            var place = CreateDefaultPlace();
            place.Close();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Close());

            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.TemporaryClosed, place.State);
        }

        [Fact]
        public void State_Close_Occupied()
        {
            var place = CreateDefaultPlace();
            place.Occupy();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Close());
            
            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Occupied, place.State);
        }

        #endregion

        #region Open

        [Fact]
        public void State_Open()
        {
            var place = CreateDefaultPlace();
            place.Close();
            place.ClearDomainEvents();

            place.Open();

            Assert.True(place.HasDomainEvent(typeof(PlaceFreedDomainEvent)));
            Assert.Equal(PlaceState.Free, place.State);
        }

        [Fact]
        public void State_Open_Freed()
        {
            var place = CreateDefaultPlace();

            Assert.Throws<DomainException>(() => place.Open());

            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Free, place.State);
        }

        [Fact]
        public void State_Open_Occupied()
        {
            var place = CreateDefaultPlace();
            place.Occupy();
            place.ClearDomainEvents();

            Assert.Throws<DomainException>(() => place.Open());
            
            Assert.True(place.HasNoDomainEvent());
            Assert.Equal(PlaceState.Occupied, place.State);
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
