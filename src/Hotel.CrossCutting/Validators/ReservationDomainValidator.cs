using System;

using Hotel.Domain.Entities;
using Hotel.Shared.Extensions;

using FluentValidation;

namespace Hotel.CrossCutting.Validators
{
    public class ReservationDomainValidator : Validator<Reservation>, Shared.Interfaces.IValidator<Reservation>
    {
        #region Constructors
        public ReservationDomainValidator()
        {
            RuleFor(p => p.CheckIn).NotNull();
            RuleFor(p => p.CheckOut).NotNull();
            RuleFor(x => x).Custom
            (
                (reservation, context) =>
                {
                    if (IsRangeDateInvalid(reservation))
                    {
                        context.AddFailure("Check the range date");
                    }

                    if (IsBookedForTodayOrBefore(reservation))
                    {
                        context.AddFailure("All reservations start at least the next day of booking.");
                    }

                    if (IsBookedForMoreThanDays(reservation))
                    {
                        context.AddFailure("The stay can’t be longer than 3 days");
                    }

                    if (IsBookedWithDaysInAdvance(reservation))
                    {
                        context.AddFailure("The stay can’t be reserved more than 30 days in advance.");
                    }                    
                }
            );
        }
        #endregion

        #region Private Methods
        private bool IsRangeDateInvalid(Reservation reservation)
        {
            return reservation.CheckIn > reservation.CheckOut || reservation.CheckOut < reservation.CheckIn;
        }

        private bool IsBookedForTodayOrBefore(Reservation reservation)
        {
            return reservation.CheckIn.StartOfDay() <= DateTime.Now.StartOfDay();
        }

        private bool IsBookedForMoreThanDays(Reservation reservation, int days = 3)
        {
            return (reservation.CheckOut - reservation.CheckIn.EndOfDay()).Days > days;
        }

        private bool IsBookedWithDaysInAdvance(Reservation reservation, int days = 30)
        {
            return (reservation.CheckIn - DateTime.Now.EndOfDay()).Days > days;
        }
        #endregion
    }
}
