using System;
using System.Threading.Tasks;

using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Services;

namespace Hotel.Domain.Services
{
    public class RoomDomainService : DomainService<Room>, IRoomDomainService
    {
        #region Constructors
        public RoomDomainService(Interfaces.Repositories.IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        #endregion

        #region Fields
        private readonly Interfaces.Repositories.IRoomRepository _roomRepository;
        #endregion

        #region Public Methods
        public async Task<Room> GetAvailableAsync(DateTime checkIn, DateTime checkOut, long? reservationId = null)
        {
            return await _roomRepository.GetAvailableAsync(checkIn, checkOut, reservationId);
        }
        #endregion
    }
}
