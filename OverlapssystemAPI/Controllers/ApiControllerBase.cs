using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssystemAPI.Common;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Handle(Result result)
        {
            if (result.Success)
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                   
                });

            return HandleFailure<object>(result.Error);
        }

        protected IActionResult Handle<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(new ApiResponse<T>
                {
                    Success = true,
                    Data = result.Value,
                });

            return HandleFailure<T>(result.Error);
        }

        private IActionResult HandleFailure<T>(Error error)
        {
            
            var response = new ApiResponse<T>
            {
                Success = false,
                Error = error?.Message ?? "En ukendt fejl opstod"
            };


            return error?.Type switch
            {
                ErrorType.NotFound => NotFound(response),
                ErrorType.Validation => BadRequest(response),
                ErrorType.Technical => StatusCode(500, response),
                _ => StatusCode(500, response)
            };
        }
    }

   
}
