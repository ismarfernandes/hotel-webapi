using Hotel.Application.Dtos.Request;
using Hotel.CrossCutting.Mappers;
using Hotel.CrossCutting.Validators;
using Hotel.Domain.Entities;
using Hotel.Shared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Hotel.CrossCutting.IoC.Profiles
{
    public class CrossCuttingProfile : DependencyProfile
    {
        #region Public Methods
        public override void Load(DependencyManagerContext builder)
        {
            // Mapper
            builder.Services.AddAutoMapper(typeof(EntityToDtoProfile), typeof(DtoToEntityProfile));

            // Validators
            builder.Services.AddTransient(typeof(IValidator<Reservation>), typeof(ReservationDomainValidator));

            builder.Services.AddTransient(typeof(IValidator<ReservationCreateRequestDto>), typeof(ReservationCreateRequestAppValidator));
            builder.Services.AddTransient(typeof(IValidator<ReservationModifyRequestDto>), typeof(ReservationModifyRequestAppValidator));
            builder.Services.AddTransient(typeof(IValidator<RoomCheckAvailabilityRequestDto>), typeof(RoomCheckAvailabilityAppValidator));
        }
        #endregion
    }
}
