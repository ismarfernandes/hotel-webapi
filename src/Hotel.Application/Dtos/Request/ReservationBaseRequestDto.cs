using System;

namespace Hotel.Application.Dtos.Request
{
    public abstract class ReservationBaseRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
