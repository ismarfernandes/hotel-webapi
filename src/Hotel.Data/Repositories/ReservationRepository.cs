using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Hotel.Data.Contexts;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        #region Constructors
        public ReservationRepository(HotelContext context) : base(context)
        {
        }
        #endregion

        #region Public Methods
        public override async Task<Reservation> FindAsync(Expression<Func<Reservation, bool>> expression = null)
        {
            if (expression == null) expression = (x) => true;

            return await _context.Reservations
                .Include(p => p.Room)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public override async Task<ICollection<Reservation>> ListAsync()
        {
            return await _context.Reservations
                .Include(p => p.Room)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion
    }
}
