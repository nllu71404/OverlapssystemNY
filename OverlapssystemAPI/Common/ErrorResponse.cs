using OverlapssytemApplication.Common.Errors;

namespace OverlapssystemAPI.Common
{
    
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public Error Error { get; set; } = default!;
    }
}
