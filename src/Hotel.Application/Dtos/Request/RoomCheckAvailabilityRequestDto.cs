using System;

namespace Hotel.Application.Dtos.Request
{
    public class RoomCheckAvailabilityRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
