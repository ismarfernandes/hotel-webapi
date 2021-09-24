using Hotel.Application.Dtos.Request;

namespace Hotel.CrossCutting.Validators
{
    public class ReservationCreateRequestAppValidator : ReservationBaseValidator<ReservationCreateRequestDto>
    {
        #region Constructors
        public ReservationCreateRequestAppValidator() : base()
        {
            
        }
        #endregion
    }
}
