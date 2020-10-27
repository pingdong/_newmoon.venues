using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueOpenedDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueOpenedDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
