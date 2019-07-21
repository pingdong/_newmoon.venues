using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class EventCanceledIntegrationEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<EventCanceledIntegrationEvent>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
