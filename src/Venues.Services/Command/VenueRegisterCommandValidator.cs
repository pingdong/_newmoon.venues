using FluentValidation;
using PingDong.Newmoon.Venues.Validations;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueRegisterCommandValidator : AbstractValidator<VenueRegisterCommand>
    {
        public VenueRegisterCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty().WithMessage("The provided name is empty");

            RuleFor(evt => evt.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(evt => evt.Address).SetValidator(new AddressValidator());
        }
    }
}
