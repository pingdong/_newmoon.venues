using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommandValidatorTest
    {
        [Fact]
        public void VenueCloseCommand_ShouldThrowException_WhenVenueIdIsEmpty()
        {
            // ARRANGE
            var cmd = new VenueCloseCommand {Id = Guid.Empty};
            var validator = new VenueCloseCommandValidator();

            // ACT
            var result = validator.Validate(cmd);
            Assert.Contains(result.Errors, o => o.PropertyName == "Id");
        }
    }
}
