using System;
using FluentValidation;

namespace PingDong.Newmoon.Places.Core.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.IsValid).Equal(true).WithMessage("The provided address is not valid");
        }

        private bool BeAValidStartDate(DateTime date)
        {
            return date.Hour >= 9;
        }
    }
}
