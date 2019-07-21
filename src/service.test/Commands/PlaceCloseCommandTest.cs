using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceCloseCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceCloseCommand>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
