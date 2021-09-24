using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Hotel.Data.Contexts;
using Hotel.Domain.Entities;
using Hotel.Domain.Enumerations;
using Hotel.Domain.Interfaces.Repositories;
using Hotel.Shared.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        #region Constructors
        public RoomRepository(HotelContext context) : base(context)
        {
        }
        #endregion

        #region Public Methods
        public async Task<Room> GetAvailableAsync(DateTime checkIn, DateTime checkOut, long? reservationId = null)
        {
            var hasSpecificReservation = reservationId.GetValueOrDefault() > 0;

            var hasReservation = await _context.Reservations.AnyAsync
                (
                    reservation =>
                        reservation.Active &&
                        reservation.Status == ReservationStatus.Reserved &&
                        reservation.CheckIn < checkOut.EndOfDay() && 
                        checkIn.StartOfDay() < reservation.CheckOut &&
                        !hasSpecificReservation ? reservation.Id != reservationId.GetValueOrDefault() : false
                );

            if (hasReservation) 
                return null;
            else 
                return await _context.Rooms
                    .Include(s => s.Reservations)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public override async Task<Room> FindAsync(Expression<Func<Room, bool>> expression = null)
        {
            if (expression == null) expression = (x) => true;
            return await _context.Rooms
                .Include(p => p.Reservations)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public override async Task<ICollection<Room>> ListAsync()
        {
            return await _context.Rooms
                .Include(p => p.Reservations)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion
    }
}
