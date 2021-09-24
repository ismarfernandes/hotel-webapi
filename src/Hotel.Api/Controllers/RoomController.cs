using System;
using System.Threading.Tasks;

using Hotel.Application.Dtos.Request;
using Hotel.Application.Interfaces;
using Hotel.Shared.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    public class RoomController : ApiController
    {
        #region Constructors
        /// <summary>
        /// RoomController Constructor, recive and set dependencies
        /// </summary>
        /// <param name="roomAppService">Room service from Dependency Injection</param>
        public RoomController(IRoomAppService roomAppService)
        {
            _roomAppService = roomAppService;
        }
        #endregion

        #region Attributes
        private readonly IRoomAppService _roomAppService;
        #endregion

        #region Public Methods       
        /// <summary>
        /// Check the availability of a reservation considering the range date
        /// </summary>
        /// <param name="checkIn">Start date</param>
        /// <param name="checkOut">End date</param>
        /// <param name="validator">Validator tha will be injected to make validations to dto RoomCheckAvailabilityRequestDto</param>
        /// <returns></returns>
        /// <response code="200">Returns avaliable room in the inputed date range</response>
        /// <response code="422">Returns validation failure messages</response>
        /// <response code="500">When there are no exceptions handled by the application</response>
        [HttpGet("CheckAvailabilityAsync")]
        public async Task<IActionResult> CheckAvailabilityAsync([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut, [FromServices] IValidator<RoomCheckAvailabilityRequestDto> validator)
        {
            var dto = new RoomCheckAvailabilityRequestDto { CheckIn = checkIn, CheckOut = checkOut };
            var response = await _roomAppService.CheckAvailabilityAsync(dto, validator);

            return Result(response);
        }
        #endregion
    }
}
