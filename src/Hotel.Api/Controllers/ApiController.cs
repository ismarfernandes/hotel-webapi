using Hotel.Shared.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class ApiController : ControllerBase
    {
        #region Protected Methods
        protected IActionResult Result(IResponse response)
        {
            return response.Type switch
            {
                Shared.Enumerators.ResponseType.Error => UnprocessableEntity(response),
                Shared.Enumerators.ResponseType.Validation => UnprocessableEntity(response),
                _ => Ok(response),
            };
        }
        #endregion
    }
}
