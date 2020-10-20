using FluentValidation.TestHelper;
using Xunit;

namespace PingDong.Newmoon.Venues.Validations
{
    public class AddressValidationTest
    {
        [Fact]
        public void InvalidNo_Should_Fail()
        {
            var address = new Address(null, "Queen St.", "Auckland", "Auckland", "New Zealand", "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldHaveAnyValidationError();
            rule.ShouldHaveValidationErrorFor(a => a.No, address);
        }

        [Fact]
        public void InvalidStreet_Should_Fail()
        {
            var address = new Address("20", null, "Auckland", "Auckland", "New Zealand", "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldHaveAnyValidationError();
            rule.ShouldHaveValidationErrorFor(a => a.Street, address);
        }

        [Fact]
        public void InvalidCity_Should_Fail()
        {
            var address = new Address("20", "Queen St.", null, "Auckland", "New Zealand", "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldHaveAnyValidationError();
            rule.ShouldHaveValidationErrorFor(a => a.City, address);
        }

        [Fact]
        public void MissingState_Should_Pass()
        {
            var address = new Address("20", "Queen St.", "Auckland", null, "New Zealand", "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void MissingCountry_Should_Pass()
        {
            var address = new Address("20", "Queen St.", "Auckland", "Auckland", null, "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void MissingZip_Should_Pass()
        {
            var address = new Address("20", "Queen St.", "Auckland", "Auckland", "New Zealand", null);

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Valid_Should_Pass()
        {
            var address = new Address("20", "Symond", "Auckland", "Auckland", "New Zealand", "0926");

            var rule = new AddressValidator();

            rule.TestValidate(address).ShouldNotHaveAnyValidationErrors();
        }
    }
}
