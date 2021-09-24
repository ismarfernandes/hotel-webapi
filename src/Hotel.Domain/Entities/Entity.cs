using System;

namespace Hotel.Domain.Entities
{
    public abstract class Entity
    {
        #region Constructors
        public Entity() { }
        public Entity(long id)
        {
            Id = id;
        }
        #endregion

        #region Fields
        public long Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } 
        #endregion
    }
}
