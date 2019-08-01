using System;
using FluentValidation;
using PingDong.Newmoon.Places.Core.Validations;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places.Service.Command
{
    public class UpdatePlaceCommandValidator : AbstractValidator<UpdatePlaceCommand>
    {
        public UpdatePlaceCommandValidator()
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
