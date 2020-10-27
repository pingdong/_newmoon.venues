using PingDong.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues
{
    public class AddressTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new DtoClassTester<Address>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
