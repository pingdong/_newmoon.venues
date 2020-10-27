using FluentValidation.TestHelper;
using PingDong.Validations;
using System;
using Xunit;

namespace PingDong.Newmoon.Venues.Validations
{
    public class VenueValidationTest
    {
        [Fact]
        public void InvalidAddress_Should_Fail()
        {
            var address = new Address("20", null, "Auckland", "Auckland", "New Zealand", "0926");
            
            Assert.Throws<ValidationFailureException>(() => new Venue("Test", address));
        }

        [Fact]
        public void Validator_Should_Have_AddressValidator()
        {
            var rule = new VenueValidator();

            rule.ShouldHaveChildValidator(p => p.Address, typeof(AddressValidator));
        }

        [Fact]
        public void MissingName_Should_Fail()
        {
            var address = new Address("20", null, "Auckland", "Auckland", "New Zealand", "0926");
            
            Assert.Throws<ArgumentNullException>(() => new Venue(null, address));
        }

        [Fact]
        public void Valid_Should_Pass()
        {
            var address = new Address("20", "Queen St.", "Auckland", "Auckland", "New Zealand", "0926");
            var valid = new Venue("Farmers", address);

            var rule = new VenueValidator();

            rule.TestValidate(valid).ShouldNotHaveAnyValidationErrors();
        }
    }
}
