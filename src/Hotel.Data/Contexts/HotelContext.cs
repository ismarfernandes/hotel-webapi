using System.Linq;
using System.Reflection;

using Hotel.Data.Extensions;
using Hotel.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hotel.Data.Contexts
{
    public class HotelContext : DbContext
    {
        #region Constructors
        public HotelContext()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
        #endregion

        #region Properties
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplySeedsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
