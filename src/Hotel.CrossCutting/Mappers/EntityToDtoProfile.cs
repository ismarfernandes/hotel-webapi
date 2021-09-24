using AutoMapper;

using Hotel.Application.Dtos.Response;
using Hotel.Domain.Entities;

namespace Hotel.CrossCutting.Mappers
{
    public class EntityToDtoProfile : Profile
    {
        #region Constructors
        public EntityToDtoProfile()
        {
            CreateMap<Room, RoomResponseDto>();
            CreateMap<Reservation, ReservationResponseDto>();
        }
        #endregion
    }
}
