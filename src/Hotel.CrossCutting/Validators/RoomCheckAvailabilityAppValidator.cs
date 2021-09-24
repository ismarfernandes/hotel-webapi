using Hotel.Application.Dtos.Request;

using FluentValidation;

namespace Hotel.CrossCutting.Validators
{
    public class RoomCheckAvailabilityAppValidator : Validator<RoomCheckAvailabilityRequestDto>, Shared.Interfaces.IValidator<RoomCheckAvailabilityRequestDto>
    {
        #region Constructors
        public RoomCheckAvailabilityAppValidator()
        {
            RuleFor(r => r.CheckIn)
                .NotNull()
                .NotEmpty()
                .WithMessage("Check-In does not be null or empty");

            RuleFor(r => r.CheckOut)
                .NotNull()
                .NotEmpty()
                .WithMessage("Check-Out does not be null or empty");

            RuleFor(x => x)
                .Custom((dto, context) => {
                    if (dto.CheckIn > dto.CheckOut || dto.CheckOut < dto.CheckIn)
                    {
                        context.AddFailure("Check the range date");
                    }
                });
        }
        #endregion
    }
}
