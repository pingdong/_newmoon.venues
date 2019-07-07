using System;
using FluentValidation.TestHelper;
using PingDong.Newmoon.Places.Core.Validations;
using Xunit;

namespace PingDong.Newmoon.Places.Core.Test.Validations
{
    public class PlaceValidationTest
    {
        [Fact]
        public void InvalidAddress_Should_Fail()
        {
            var address = new Address("20", null, "Auckland", "Auckland", "New Zealand", "0926");
            var place = new Place("Test", address);
            
            var rule = new PlaceValidator();

            rule.TestValidate(place).ShouldHaveError();
            rule.ShouldHaveValidationErrorFor(p => p.Address.Street, place);
        }

        [Fact]
        public void Validator_Should_Have_AddressValidator()
        {
            var rule = new PlaceValidator();

            rule.ShouldHaveChildValidator(p => p.Address, typeof(AddressValidator));
        }

        [Fact]
        public void MissingName_Should_Fail()
        {
            var address = new Address("20", null, "Auckland", "Auckland", "New Zealand", "0926");
            Assert.Throws<ArgumentNullException>(() => new Place(null, address));
        }

        [Fact]
        public void Valid_Should_Pass()
        {
            var address = new Address("20", "Queen St.", "Auckland", "Auckland", "New Zealand", "0926");
            var valid = new Place("Farmers", address);

            var rule = new PlaceValidator();

            rule.TestValidate(valid).ShouldNotHaveError();
        }
    }
}
