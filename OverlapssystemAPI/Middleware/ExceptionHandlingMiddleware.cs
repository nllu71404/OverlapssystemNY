namespace OverlapssystemAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                error = "Der opstod en uventet fejl. Prøv igen senere."
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}

