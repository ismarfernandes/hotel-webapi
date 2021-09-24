using System.Threading.Tasks;

using Hotel.Application.Dtos.Request;
using Hotel.Domain.Entities;
using Hotel.Shared.Interfaces;

namespace Hotel.Application.Interfaces
{
    public interface IRoomAppService : IAppService<Room>
    {
        Task<IResponse> CheckAvailabilityAsync(RoomCheckAvailabilityRequestDto dto, IValidator<RoomCheckAvailabilityRequestDto> validator);
    }
}
