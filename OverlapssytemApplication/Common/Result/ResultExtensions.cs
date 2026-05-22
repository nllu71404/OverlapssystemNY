using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Result
{
    public static class ResultExtensions
    {
        // Transformerer et Result<TIn> til et Result<TOut> ved at anvende en mapper-funktion på success-værdien.
        // Ved success transformeres værdien TIn til TOut.
        // Ved failure returneres fejlen uændret.

        // Bruges i facades og controllers for at reducere eksplicit branching (if/else).
        public static Result<TOut> Map<TIn, TOut>(
    this Result<TIn> result,
    Func<TIn, TOut> mapper)
        {
            return result.Match(
                value => Result.Ok(mapper(value)),
                error => error
            );
        }
    }
}
