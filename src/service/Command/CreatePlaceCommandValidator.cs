using FluentValidation;
using PingDong.Newmoon.Places.Core.Validations;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places.Service.Command
{
    public class CreatePlaceCommandValidator : AbstractValidator<CreatePlaceCommand>
    {
        public CreatePlaceCommandValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty().NotEmpty().WithMessage("The provided name is empty");

            RuleFor(evt => evt.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(evt => evt.Address).SetValidator(new AddressValidator());
        }
    }
}
