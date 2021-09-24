using System;
using System.Threading.Tasks;

using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> GetAvailableAsync(DateTime checkIn, DateTime checkOut, long? reservationId = null);
    }
}
