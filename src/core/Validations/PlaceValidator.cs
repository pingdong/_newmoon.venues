using FluentValidation;

namespace PingDong.Newmoon.Places.Core.Validations
{
    public class PlaceValidator : AbstractValidator<Place>
    {
        public PlaceValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(evt => evt.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(evt => evt.Address).SetValidator(new AddressValidator());
        }
    }
}
