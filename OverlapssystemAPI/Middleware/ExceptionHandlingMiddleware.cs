using OverlapssystemAPI.Common;
using OverlapssytemApplication.Common.Errors;

namespace OverlapssystemAPI.Middleware
{
    // Middleware til global håndtering af uventede fejl i API'en
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Fanger alle exceptions, logger dem og returnerer en generisk fejlbesked til klienten
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                ex,
                "Uventet fejl i API {Method} {Path} | TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier
                );

                await HandleExceptionAsync(context);
            }
        }

        // Returnerer en standardiseret JSON-fejlbesked til klienten ved uventede fejl
        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorResponse
            {
                Success = false,
                Error = Error.Technical("Der opstod en uventet fejl. Prøv igen senere.")
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}

