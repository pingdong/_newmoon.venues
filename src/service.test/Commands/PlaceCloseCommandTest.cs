using PingDong.CleanArchitect.Core.Testing;
using PingDong.Newmoon.Places.Service.Commands;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Test
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
