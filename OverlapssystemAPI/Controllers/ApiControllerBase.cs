using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Common.Errors;
using OverlapssystemAPI.Common;
using Microsoft.AspNetCore.Authorization;
using OverlapssytemApplication.Common.Result;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Handle(Result result)
        {
            return result.Match<IActionResult>(
                onSuccess: () => Ok(new ApiResponse<object>
                {
                    Success = true
                }),

                onFailure: error => HandleFailure<object>(error)
            );
        }

        protected IActionResult Handle<T>(Result<T> result)
        {
            return result.Match<IActionResult>(
                onSuccess: value => Ok(new ApiResponse<T>
                {
                    Success = true,
                    Data = value
                }),

                onFailure: error => HandleFailure<T>(error)
            );
        }

        private IActionResult HandleFailure<T>(Error error)
        {
            var response = new ErrorResponse
            {
                Success = false,
                Error = error 
            };

            return error.Type switch
            {
                ErrorType.NotFound => NotFound(response),
                ErrorType.Validation => BadRequest(response),
                ErrorType.Technical => StatusCode(500, response),
                _ => StatusCode(500, response)
            };
        }
    }
}
