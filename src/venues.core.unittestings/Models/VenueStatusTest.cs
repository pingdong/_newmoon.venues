using PingDong.CQRS;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueStatusTest
    {
        [Fact]
        public void FromString()
        {
            Assert.Equal(VenueStatus.Vacancy, VenueStatus.From("Vacancy"));
            Assert.Equal(VenueStatus.Occupied, VenueStatus.From("Occupied"));
            Assert.Equal(VenueStatus.Available, VenueStatus.From("Available"));
            Assert.Equal(VenueStatus.Unavailable, VenueStatus.From("Unavailable"));
        }

        [Fact]
        public void FromString_Invalid()
        {
            Assert.Throws<EntityException>(() => VenueStatus.From("Test"));
        }

        [Fact]
        public void FromInt()
        {
            Assert.Equal(VenueStatus.Vacancy, VenueStatus.From(1));
            Assert.Equal(VenueStatus.Occupied, VenueStatus.From(2));
            Assert.Equal(VenueStatus.Available, VenueStatus.From(3));
            Assert.Equal(VenueStatus.Unavailable, VenueStatus.From(4));
        }

        [Fact]
        public void FromInt_Invalid()
        {
            Assert.Throws<EntityException>(() => VenueStatus.From(999));
        }
    }
}
