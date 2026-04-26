using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Handle(Result result)
        {
            if (result.Success)
                return Ok();

            return HandleFailure(result);
        }

        protected IActionResult Handle<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(result.Value);

            return HandleFailure(result);
        }

        private IActionResult HandleFailure(Result result)
        {
            if (result.Error == null)
                return StatusCode(500, "En ukendt fejl opstod");

            return result.Error.Type switch
            {
                ErrorType.NotFound => NotFound(result.Error.Message),
                ErrorType.Validation => BadRequest(result.Error.Message),
                ErrorType.Technical => StatusCode(500, result.Error.Message),
                _ => StatusCode(500, result.Error.Message)
            };
        }
    }
}
