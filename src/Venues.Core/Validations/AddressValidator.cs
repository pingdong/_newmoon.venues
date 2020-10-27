using FluentValidation;

namespace PingDong.Newmoon.Venues.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.No).NotEmpty().NotEmpty().WithMessage("The provided address doesn't have a valid 'No'");
            RuleFor(evt => evt.Street).NotEmpty().WithMessage("The provided address doesn't have a valid 'Street'");
            RuleFor(evt => evt.City).NotEmpty().WithMessage("The provided address doesn't have a valid 'City'");
        }
    }
}
