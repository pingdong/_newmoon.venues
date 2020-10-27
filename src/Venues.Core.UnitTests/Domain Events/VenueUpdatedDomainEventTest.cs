using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueUpdatedDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueUpdatedDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
