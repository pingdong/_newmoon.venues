using PingDong.CleanArchitect.Core.Testing;
using PingDong.Newmoon.Places.Service.Commands;
using Xunit;

namespace PingDong.Newmoon.Places.Core.Test
{
    public class PlaceDisengageCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceDisengageCommand>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
