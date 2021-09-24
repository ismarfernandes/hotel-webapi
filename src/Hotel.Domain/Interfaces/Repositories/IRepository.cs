using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        #region Public Methods
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(long id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<TEntity>> ListAsync();
        #endregion
    }
}
