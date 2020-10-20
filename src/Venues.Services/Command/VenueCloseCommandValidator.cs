using FluentValidation;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommandValidator : AbstractValidator<VenueCloseCommand>
    {
        public VenueCloseCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Id).NotEmpty().WithMessage("The provided Id is empty");
        }
    }
}
