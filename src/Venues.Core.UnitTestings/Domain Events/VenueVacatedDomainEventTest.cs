using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class VenueVacatedDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueVacatedDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
