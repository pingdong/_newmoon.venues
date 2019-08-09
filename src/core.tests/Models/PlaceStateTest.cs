using PingDong.CleanArchitect.Core;
using Xunit;

namespace PingDong.Newmoon.Places.Core.Test
{
    public class PlaceStateTest
    {
        [Fact]
        public void FromString()
        {
            Assert.Equal(PlaceState.TemporaryClosed, PlaceState.From("TemporaryClosed"));
            Assert.Equal(PlaceState.Free, PlaceState.From("Free"));
            Assert.Equal(PlaceState.Occupied, PlaceState.From("Occupied"));
        }

        [Fact]
        public void FromString_Invalid()
        {
            Assert.Throws<EntityException>(() => PlaceState.From("Test"));
        }

        [Fact]
        public void FromInt()
        {
            Assert.Equal(PlaceState.TemporaryClosed, PlaceState.From(3));
            Assert.Equal(PlaceState.Free, PlaceState.From(1));
            Assert.Equal(PlaceState.Occupied, PlaceState.From(2));
        }

        [Fact]
        public void FromInt_Invalid()
        {
            Assert.Throws<EntityException>(() => PlaceState.From(999));
        }
    }
}
