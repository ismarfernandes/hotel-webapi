using AutoMapper;

using Hotel.Application.Dtos.Request;
using Hotel.Domain.Entities;

namespace Hotel.CrossCutting.Mappers
{
    public class DtoToEntityProfile : Profile
    {
        #region Constructors
        public DtoToEntityProfile()
        {
            CreateMap<ReservationCreateRequestDto, Reservation>();
            CreateMap<ReservationModifyRequestDto, Reservation>();
        }
        #endregion
    }
}
