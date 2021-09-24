using System;

namespace Hotel.Application.Dtos.Response
{
    public abstract class EntityResponseDto
    {
        #region Fields
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
