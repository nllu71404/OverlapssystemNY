using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Errors
{
    // Error klassen repræsenterer en fejl, der kan opstå i applikationen.
    // Den indeholder en fejlmeddelelse og en type, der angiver, hvilken slags fejl det er (f.eks. valideringsfejl, ikke fundet, teknisk fejl).
    //Vi bruger typerne til at kunne håndtere forskellige fejltyper forskelligt i vores applikation, f.eks. ved at vise forskellige beskeder til brugeren eller logge dem på forskellige måder.
    public class Error
    {
        public string Message { get; init; } = string.Empty;

        public ErrorType Type { get; init; }

        public Error()
        {
        }

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
