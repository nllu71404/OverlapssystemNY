using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Result
{
    public static class ResultExtensions
    {
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
