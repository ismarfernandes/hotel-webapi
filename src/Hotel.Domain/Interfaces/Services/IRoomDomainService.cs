using System;
using System.Threading.Tasks;

using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Services
{
    public interface IRoomDomainService
    {
        /// <summary>
        /// Based on period of dates check if there is a room available 
        /// </summary>
        /// <param name="checkIn">Start date of reservation</param>
        /// <param name="checkOut">End date of reservation</param>
        /// <returns>A </returns>
        Task<Room> GetAvailableAsync(DateTime checkIn, DateTime checkOut, long? reservationId = null);
    }
}
