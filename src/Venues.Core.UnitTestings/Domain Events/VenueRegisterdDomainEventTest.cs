using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueRegisteredDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueRegisteredDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
