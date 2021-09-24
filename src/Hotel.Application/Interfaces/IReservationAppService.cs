using System.Threading.Tasks;

using Hotel.Application.Dtos.Request;
using Hotel.Domain.Entities;
using Hotel.Shared.Interfaces;

namespace Hotel.Application.Interfaces
{
    public interface IReservationAppService : IAppService<Reservation>
    {
        Task<IResponse> MakeAsync(ReservationCreateRequestDto dto, IValidator<ReservationCreateRequestDto> validator);
        Task<IResponse> CancelAsync(long id);
        Task<IResponse> ModifyAsync(long id, ReservationModifyRequestDto dto, IValidator<ReservationModifyRequestDto> validator);
    }
}
