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
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Constructors
        public Repository(HotelContext context)
        {
            _context = context;
        } 
        #endregion

        #region Fields
        protected readonly HotelContext _context;
        #endregion

        #region Public Methods
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = _context.Update(entity);
            
            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        public virtual async Task<TEntity> FindByIdAsync(long id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null) return null;

            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);

            entity.Active = false;

            _ = _context.Set<TEntity>().Update(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<ICollection<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion
    }
}
