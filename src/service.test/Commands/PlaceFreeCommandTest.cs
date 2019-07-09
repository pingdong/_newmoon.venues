using PingDong.CleanArchitect.Core.Testing;
using PingDong.Newmoon.Places.Service.Commands;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Test
{
    public class PlaceFreeCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceFreeCommand>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
