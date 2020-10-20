using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class VenueClosedIntegrationEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueClosedIntegrationEvent>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
