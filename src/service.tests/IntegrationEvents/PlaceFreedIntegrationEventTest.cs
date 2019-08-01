using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class PlaceFreedIntegrationEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceFreedIntegrationEvent>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
