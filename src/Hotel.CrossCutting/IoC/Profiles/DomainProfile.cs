using Hotel.Domain.Interfaces.Services;
using Hotel.Domain.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Hotel.CrossCutting.IoC.Profiles
{
    public class DomainProfile : DependencyProfile
    {
        #region Public Methodss
        public override void Load(DependencyManagerContext builder)
        {
            builder.Services.AddTransient<IRoomDomainService, RoomDomainService>();            
            builder.Services.AddTransient<IReservationDomainService, ReservationDomainService>();            
        } 
        #endregion
    }
}
