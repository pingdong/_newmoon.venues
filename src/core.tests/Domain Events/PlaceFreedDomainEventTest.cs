using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Core.Test
{
    public class PlaceFreedDomainEventTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceFreedDomainEvent>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
