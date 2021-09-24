using System;

namespace Hotel.Application.Dtos.Response
{
    public class ReservationResponseDto : EntityResponseDto
    {
        #region Fields
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
        public RoomResponseDto Room { get; set; }
        #endregion
    }
}
