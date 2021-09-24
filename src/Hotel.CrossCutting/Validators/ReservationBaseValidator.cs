using Hotel.Application.Dtos.Request;

using FluentValidation;

namespace Hotel.CrossCutting.Validators
{
    public abstract class ReservationBaseValidator<T> : Validator<T>, Shared.Interfaces.IValidator<T> where T : ReservationBaseRequestDto
    {
        #region Constructors
        public ReservationBaseValidator()
        {
            RuleFor(p => p.CheckIn).NotNull();
            RuleFor(p => p.CheckOut).NotNull();
            RuleFor(x => x).Custom
            (
                (dto, context) =>
                {
                    if (dto.CheckIn > dto.CheckOut || dto.CheckOut < dto.CheckIn)
                    {
                        context.AddFailure("Check the range date");
                    }
                }
            );
        }
        #endregion
    }
}
