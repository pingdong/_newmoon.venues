using PingDong.CleanArchitect.Core.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.Core.Test
{
    public class AddressTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<Address>();
            
            Assert.True(tester.VerifyPropertiesAssignedFromConstructor());
        }
    }
}
