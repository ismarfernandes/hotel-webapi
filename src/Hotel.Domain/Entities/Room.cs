using System.Collections.Generic;

using Hotel.Domain.Enumerations;

namespace Hotel.Domain.Entities
{
    public class Room : Entity
    {
        #region Constructors
        public Room() { }

        public Room(long id) : base(id)
        {

        }
        #endregion

        #region Fields
        public int Number { get; set; }
        public string Description { get; set; }
        public RoomTypes Type { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } 
        #endregion
    }
}
