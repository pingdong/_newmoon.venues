using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOpenCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceOpenCommand>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
