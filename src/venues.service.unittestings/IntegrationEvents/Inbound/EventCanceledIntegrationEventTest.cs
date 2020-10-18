using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class EventCanceledIntegrationEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<EventCanceledIntegrationEvent>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
