using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class PlaceClosedIntegrationEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceClosedIntegrationEvent>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
