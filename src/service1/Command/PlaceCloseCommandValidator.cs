using System;
using FluentValidation;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places.Service.Command
{
    public class PlaceCloseCommandValidator : AbstractValidator<PlaceCloseCommand>
    {
        public PlaceCloseCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Id).NotEmpty().WithMessage("The provided Id is empty");
            RuleFor(evt => evt.Id).NotEqual(Guid.Empty).WithMessage("The provided Id is invalid");
        }
    }
}
