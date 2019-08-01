using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
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
