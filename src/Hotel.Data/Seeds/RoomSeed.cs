using System;

using Hotel.Data.Interfaces;
using Hotel.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Seeds
{
    public class RoomSeed : ISeed
    {
        public void Populate(ModelBuilder modelBuilder)
        {
            var lastRoom = new Room(1)
            {
                Active = true,
                CreatedAt = DateTime.Now,
                Description = "The last room",
                Number = 1,
                Type = Domain.Enumerations.RoomTypes.Standard,
                UpdatedAt = null
            };

            modelBuilder.Entity<Room>().HasData(lastRoom);
        }
    }
}
