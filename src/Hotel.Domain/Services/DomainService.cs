using System;

using Hotel.Domain.Entities;

namespace Hotel.Domain.Services
{
    public abstract class DomainService<TEntity> where TEntity : Entity
    {
        #region Protected Methods
        /// <summary>
        /// If an entity will be added, fill basic required information like: Active, CreatedDate, etc
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void GenerateDefaultValuesOnAdd(TEntity entity)
        {
            entity.Active = true;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = null;
        }

        /// <summary>
        /// If an entity will be modified, fill basic required informations like: UpdatedAt
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void GenerateDefaultValuesOnUpdate(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
        } 
        #endregion
    }
}
