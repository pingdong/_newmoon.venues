using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<VenueCloseCommand>();

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
