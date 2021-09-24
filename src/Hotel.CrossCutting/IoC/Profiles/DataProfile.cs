using Hotel.Data.Contexts;
using Hotel.Data.Repositories;
using Hotel.Domain.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.CrossCutting.IoC.Profiles
{
    public class DataProfile : DependencyProfile
    {
        #region Public Methods
        public override void Load(DependencyManagerContext builder)
        {
            //builder.Services.AddDbContext<HotelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IRoomRepository, RoomRepository>();
            builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
        }
        #endregion
    }
}
