using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Hotel.Application.Dtos.Request;
using Hotel.Application.Dtos.Response;
using Hotel.Application.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repositories;
using Hotel.Domain.Interfaces.Services;
using Hotel.Shared.Extensions;
using Hotel.Shared.Interfaces;

namespace Hotel.Application.Services
{
    public class ReservationAppService : AppService<Reservation>, IReservationAppService
    {
        #region Constructors
        public ReservationAppService
        (
            IMapper mapper,
            IReservationRepository reseservationRepository,
            IReservationDomainService reservationDomainService
        ) : base(mapper)
        {
            _reseservationRepository = reseservationRepository;
            _reservationDomainService = reservationDomainService;
        }
        #endregion

        #region Fields
        private readonly IReservationRepository _reseservationRepository; 
        private readonly IReservationDomainService _reservationDomainService; 
        #endregion

        #region Public Methods
        public async Task<IResponse> CancelAsync(long id)
        {
            var reservation = await _reseservationRepository.FindByIdAsync(id);

            if (reservation == null) return "Reservation does not found".ValidationFailureResponse();

            await _reservationDomainService.CancelAsync(reservation);

            await _reseservationRepository.UpdateAsync(reservation);

            var result = _mapper.Map<ReservationResponseDto>(reservation);

            return result.SuccessResponse();
        }

        public async Task<IResponse> MakeAsync(ReservationCreateRequestDto dto, IValidator<ReservationCreateRequestDto> validator)
        {
            try
            {
                var validations = await validator.ValidationAsync(dto);

                if (!validations.Success) return validations;

                var reservation = _mapper.Map<Reservation>(dto);

                await _reservationDomainService.MakeAsync(reservation);

                var result = _mapper.Map<ReservationResponseDto>(reservation);

                return result.SuccessResponse();
            }
            catch (AggregateException ex)
            {
                return ex.InnerExceptions.Select(e => e.Message).ValidationFailureResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResponse> ModifyAsync(long id, ReservationModifyRequestDto dto, IValidator<ReservationModifyRequestDto> validator)
        {
            try
            {
                var validations = await validator.ValidationAsync(dto);

                if (!validations.Success) return validations;

                var reservation = await _reseservationRepository.FindByIdAsync(id);

                if (reservation == null) return "Reservation does not found".ValidationFailureResponse();

                reservation = _mapper.Map(dto, reservation);

                await _reservationDomainService.ModifyAsync(reservation);

                var result = _mapper.Map<ReservationResponseDto>(reservation);

                return result.SuccessResponse();
            }
            catch (AggregateException ex)
            {
                return ex.InnerExceptions.Select(e => e.Message).ValidationFailureResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
