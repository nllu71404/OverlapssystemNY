using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Errors
{
    public class Error
    {
        public string Message { get; }
        public ErrorType Type { get; }

        private Error(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }

        public static Error Validation(string msg) => new(msg, ErrorType.Validation);
        public static Error NotFound(string msg) => new(msg, ErrorType.NotFound);
        public static Error Technical(string msg) => new(msg, ErrorType.Technical);
    }
}
