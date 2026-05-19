using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Result
{
    public static class ResultExtensions
    {
        //Denne metode tager et Result<TIn> og en mapper funktion, og returnerer et Result<TOut>.
        //Hvis Result<TIn> er en succes, vil mapper funktionen blive anvendt på værdien og returnere en succes med den nye værdi.
        //Hvis Result<TIn> er en fejl, vil fejlen blive returneret uden ændring.

        //Bruges i vores facades og controllers, hvor vi mapper, så vi undgår for meget boilerplate kode (Vi undgår if statements) 
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
