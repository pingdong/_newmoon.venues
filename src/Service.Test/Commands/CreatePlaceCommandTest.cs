using System;
using System.Collections.Generic;
using PingDong.CleanArchitect.Core.Testing;
using PingDong.Newmoon.Places.Core;
using Xunit;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommandTest
    {
        [Fact]
        public void ConstructorAssignedProperties()
        {
            var tester = new ClassTester<CreatePlaceCommand>();

            var generator = new Dictionary<string, Func<object>>
            {
                {"address", () => new Address("1", "Queen St.", "Auckland", "AKL", "NZ", "0610")}
            };

            Assert.True(tester.VerifyPropertiesAssignedFromConstructor(valueGenerator: generator));
        }
    }
}
