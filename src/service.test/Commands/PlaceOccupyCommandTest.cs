using PingDong.CleanArchitect.Core.Testing;
using PingDong.Newmoon.Places.Service.Commands;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Test
{
    public class PlaceOccupyCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<PlaceOccupyCommand>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
