using Hotel.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Data.Configurations
{
    public class RoomConfiguration : EntityConfiguration<Room>
    {
        #region Public Methods
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rooms");
            builder.Property(e => e.Number).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Type).IsRequired();

            builder.HasMany(e => e.Reservations).WithOne(f => f.Room).HasForeignKey(f => f.RoomId);
        }
        #endregion
    }
}
