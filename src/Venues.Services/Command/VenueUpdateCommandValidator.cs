using FluentValidation;
using PingDong.Newmoon.Venues.Validations;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueUpdateCommandValidator : AbstractValidator<VenueUpdateCommand>
    {
        public VenueUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Id).NotEmpty().WithMessage("The provided Id is empty");
            RuleFor(evt => evt.Id).NotEqual(Guid.Empty).WithMessage("The provided Id is invalid");

            RuleFor(evt => evt.Name).NotEmpty().NotEmpty().WithMessage("The provided name is empty");

            RuleFor(evt => evt.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(evt => evt.Address).SetValidator(new AddressValidator());
        }
    }
}
