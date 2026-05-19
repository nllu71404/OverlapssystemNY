using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Errors
{
    //Enum for kategorisering af fejltyper, som kan bruges i Error-klassen til at specificere typen af fejl, der opstod.
    public enum ErrorType
    {
        Validation,
        NotFound,
        Technical
    }
}
