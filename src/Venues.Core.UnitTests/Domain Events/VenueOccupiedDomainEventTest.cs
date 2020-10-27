using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueOccupiedDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueOccupiedDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
