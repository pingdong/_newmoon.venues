using FluentValidation;

namespace PingDong.Newmoon.Venues.Validations
{
    public class VenueValidator : AbstractValidator<Venue>
    {
        public VenueValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(evt => evt.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(evt => evt.Address).SetValidator(new AddressValidator());
        }
    }
}
