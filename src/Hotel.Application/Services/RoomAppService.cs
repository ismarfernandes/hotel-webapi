using System.Threading.Tasks;

using AutoMapper;

using Hotel.Application.Dtos.Request;
using Hotel.Application.Dtos.Response;
using Hotel.Application.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Services;
using Hotel.Shared.Extensions;
using Hotel.Shared.Interfaces;

namespace Hotel.Application.Services
{
    public class RoomAppService : AppService<Room>, IRoomAppService
    {
        #region Constructors
        public RoomAppService
        (
            IMapper mapper,
            IRoomDomainService roomDomainService
        ) : base(mapper)
        {
            _roomDomainService = roomDomainService;
        }
        #endregion
        
        #region Fields
        private readonly IRoomDomainService _roomDomainService; 
        #endregion

        #region Public Methods
        public async Task<IResponse> CheckAvailabilityAsync(RoomCheckAvailabilityRequestDto dto, IValidator<RoomCheckAvailabilityRequestDto> validator)
        {
            var validation = await validator.ValidationAsync(dto);

            if (!validation.Success) return validation;

            var room = await _roomDomainService.GetAvailableAsync(dto.CheckIn, dto.CheckOut);

            var result = _mapper.Map<RoomResponseDto>(room);

            var message = result != null ? null : "No available rooms were found in the date range.";

            return result.SuccessResponse(message);
        }
        #endregion
    }
}
