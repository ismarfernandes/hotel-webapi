using Hotel.Application.Dtos.Request;

using FluentValidation;

namespace Hotel.CrossCutting.Validators
{
    public class ReservationModifyRequestAppValidator : ReservationBaseValidator<ReservationModifyRequestDto>
    {
        #region Constructors
        public ReservationModifyRequestAppValidator() : base()
        {

        }
        #endregion
    }
}
