using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Core.Testing;

namespace PingDong.Newmoon.Places.Core
{
    [TestClass]
    public class PlaceTest
    {
        #region Occupied

        #region Init

        [TestMethod]
        public void Status_AfterCreated()
        {
            var place = CreateDefaultPlace();

            Assert.IsFalse(place.IsOccupied);
        }

        #endregion

        #region Engage

        [TestMethod]
        public void Engage()
        {
            var place = CreateDefaultPlace();

            Assert.IsFalse(place.IsOccupied);
            place.Engage();
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceStateChangedDomainEvent)));
            Assert.IsTrue(place.IsOccupied);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Engage_Occupied()
        {
            var place = CreateDefaultPlace();

            place.Engage();
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceStateChangedDomainEvent)));
            Assert.IsTrue(place.IsOccupied);

            place.Engage();
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceStateChangedDomainEvent)));
        }

        #endregion

        #region Disengage

        [TestMethod]
        public void Disengage()
        {
            var place = CreateDefaultPlace();

            place.Engage();
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceStateChangedDomainEvent)));
            Assert.IsTrue(place.IsOccupied);

            place.Disengage();
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceStateChangedDomainEvent), 2));
            Assert.IsFalse(place.IsOccupied);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Disengage_Unoccupied()
        {
            var place = CreateDefaultPlace();

            Assert.IsFalse(place.IsOccupied);
            place.Disengage();
            Assert.IsTrue(place.HasNoDomainEvent());
        }

        #endregion

        #endregion

        #region Properties

        [TestMethod]
        public void Properties()
        {
            var address = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
            var name = "Test";

            var place = new Place(name, address);

            Assert.AreEqual(name, place.Name);
            Assert.AreEqual(address, place.Address);
            Assert.IsFalse(place.IsOccupied);
        }


        [TestMethod]
        public void Properties_Update()
        {
            var address = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
            var name = "Test";

            var place = CreateDefaultPlace();

            Assert.AreNotEqual(name, place.Name);
            Assert.AreNotEqual(address, place.Address);

            place.Update(name, address);
            Assert.AreEqual(name, place.Name);
            Assert.AreEqual(address, place.Address);
            Assert.IsFalse(place.IsOccupied);
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceUpdatedDomainEvent)));
        }

        #endregion

        #region Helper

        private Place CreateDefaultPlace()
        {
            return new Place("Default", new Address("20", "Symond", "Auckland", "Auckland", "New Zealand", "0926"));
        }

        #endregion
    }
}
