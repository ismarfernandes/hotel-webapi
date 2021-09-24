using System.Threading.Tasks;

using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Services
{
    public interface IReservationDomainService
    {
        /// <summary>
        /// Change a reservation status to Canceled
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        Task CancelAsync(Reservation reservation);
        /// <summary>
        /// Modify a reservation Check-In and Check-Out date
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        Task<Reservation> ModifyAsync(Reservation reservation);
        /// <summary>
        /// Make a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        Task<Reservation> MakeAsync(Reservation reservation);
    }
}
