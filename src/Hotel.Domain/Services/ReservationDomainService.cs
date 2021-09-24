using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repositories;
using Hotel.Domain.Interfaces.Services;
using Hotel.Shared.Interfaces;

namespace Hotel.Domain.Services
{
    public class ReservationDomainService : DomainService<Reservation>, IReservationDomainService
    {
        #region Constructors
        public ReservationDomainService
        (
            IValidator<Reservation> validator,
            IRoomDomainService roomDomainService,
            IReservationRepository reseservationRepository
        )
        {
            _validator = validator;
            _roomDomainService = roomDomainService;
            _reseservationRepository = reseservationRepository;
        }
        #endregion

        #region Fields
        private readonly IValidator<Reservation> _validator;
        private readonly IRoomDomainService _roomDomainService;
        private readonly IReservationRepository _reseservationRepository;
        #endregion

        #region Public Methods
        public Task CancelAsync(Reservation reservation)
        {
            if (reservation.Active && reservation.Status != Enumerations.ReservationStatus.Canceled)
            {
                reservation.Status = Enumerations.ReservationStatus.Canceled;
            }

            return Task.CompletedTask;
        }

        public async Task<Reservation> ModifyAsync(Reservation reservation)
        {
            var validationResult = await _validator.ValidationAsync(reservation);

            if (!validationResult.Success)
            {
                var exceptions = ResponseToExceptions(validationResult);
                throw new AggregateException(exceptions);
            }

            var room = await _roomDomainService.GetAvailableAsync(reservation.CheckIn, reservation.CheckOut, reservation.Id);

            if (room == null) throw new Exception("Does not exists rooms available in this range date");

            GenerateDefaultValuesOnUpdate(reservation);

            reservation.RoomId = room.Id;

            return await _reseservationRepository.UpdateAsync(reservation);
        }

        public async Task<Reservation> MakeAsync(Reservation reservation)
        {
            var validationResult = await _validator.ValidationAsync(reservation);

            if (!validationResult.Success)
            {
                var exceptions = ResponseToExceptions(validationResult);
                throw new AggregateException(exceptions);
            }

            var room = await _roomDomainService.GetAvailableAsync(reservation.CheckIn, reservation.CheckOut);

            if (room == null) throw new Exception("Does not exists rooms available in this range date");

            reservation.RoomId = room.Id;
            reservation.Status = Enumerations.ReservationStatus.Reserved;

            GenerateDefaultValuesOnAdd(reservation);

            return await _reseservationRepository.AddAsync(reservation);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Transform the result of IValidator to a list of exceptions
        /// </summary>
        /// <param name="validationResult">Result of processed validations from IValidator</param>
        /// <returns>A list of exceptions</returns>
        private static IEnumerable<Exception> ResponseToExceptions(IResponse validationResult)
        {
            return ((IEnumerable<string>)validationResult.Result).Select(v => new ArgumentException(v));
        }
        #endregion
    }
}
