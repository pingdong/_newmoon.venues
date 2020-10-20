using FluentValidation;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueVacateCommandValidator : AbstractValidator<VenueVacateCommand>
    {
        public VenueVacateCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Id).NotEmpty().WithMessage("The provided Id is empty");
            RuleFor(evt => evt.Id).NotEqual(Guid.Empty).WithMessage("The provided Id is invalid");
        }
    }
}
