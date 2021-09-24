using System;

using Hotel.Domain.Enumerations;

namespace Hotel.Domain.Entities
{
    public class Reservation : Entity
    {
        #region Constructors
        public Reservation() { }

        public Reservation(long id) : base(id)
        {

        }
        #endregion

        #region Fields
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public ReservationStatus Status { get; set; }
        public long RoomId { get; set; }
        public Room Room { get; set; } 
        #endregion
    }
}
