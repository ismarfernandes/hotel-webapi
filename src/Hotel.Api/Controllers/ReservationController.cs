using System.Threading.Tasks;

using Hotel.Application.Dtos.Request;
using Hotel.Application.Interfaces;
using Hotel.Shared.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    public class ReservationController : ApiController
    {
        #region Constructors
        public ReservationController(IReservationAppService reservationAppService)
        {
            _reservationAppService = reservationAppService;
        }
        #endregion

        #region Fields
        private readonly IReservationAppService _reservationAppService;
        #endregion

        #region Public Methods
        /// <summary>
        /// Make a reservation
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="validator">Validator tha will be injected to make validations to dto ReservationCreateRequestDto</param>
        /// <returns></returns>
        /// <response code="200">Returns a booking</response>
        /// <response code="422">Returns validation failure messages</response>
        /// <response code="500">When there are no exceptions handled by the application</response>
        [HttpPost("MakeAsync")]
        public async Task<IActionResult> MakeAsync([FromBody] ReservationCreateRequestDto dto, [FromServices] IValidator<ReservationCreateRequestDto> validator)
        {
            var result = await _reservationAppService.MakeAsync(dto, validator);

            return Result(result);
        }

        /// <summary>
        /// Modify a booking
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <param name="dto"></param>
        /// <param name="validator">Validator tha will be injected to make validations to dto ReservationModifyRequestDto</param>
        /// <returns></returns>
        /// <response code="200">Returns a booking modified</response>
        /// <response code="422">Returns validation failure messages</response>
        /// <response code="500">When there are no exceptions handled by the application</response>
        [HttpPut("ModifyAsync/{id}")]
        public async Task<IActionResult> ModifyAsync([FromRoute] long id, [FromBody] ReservationModifyRequestDto dto, [FromServices] IValidator<ReservationModifyRequestDto> validator)
        {
            var response = await _reservationAppService.ModifyAsync(id, dto, validator);

            return Result(response);
        }

        /// <summary>
        /// Cancel a booking
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <returns></returns>
        /// <response code="200">Returns a booking with status canceled</response>
        /// <response code="422">Returns validation failure messages</response>
        /// <response code="500">When there are no exceptions handled by the application</response>
        [HttpDelete("CancelAsync/{id}")]
        public async Task<IActionResult> CancelAsync([FromRoute] long id)
        {
            var response = await _reservationAppService.CancelAsync(id);

            return Result(response);
        }
        #endregion
    }
}
