using Hotel.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Data.Configurations
{
    public class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        #region Public Methods
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            builder.ToTable("Reservations");
            builder.Property(e => e.CheckIn).IsRequired();
            builder.Property(e => e.CheckOut).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(x => x.RoomId).ValueGeneratedNever();

            builder.HasOne(e => e.Room).WithMany(f => f.Reservations).HasForeignKey(f => f.RoomId).IsRequired();
        }
        #endregion
    }
}
