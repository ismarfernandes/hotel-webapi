using Hotel.Application.Interfaces;
using Hotel.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Hotel.CrossCutting.IoC.Profiles
{
    public class ApplicationProfile : DependencyProfile
    {
        #region Public Methods
        public override void Load(DependencyManagerContext builder)
        {
            builder.Services.AddTransient<IRoomAppService, RoomAppService>();
            builder.Services.AddTransient<IReservationAppService, ReservationAppService>();
        }
        #endregion
    }
}
