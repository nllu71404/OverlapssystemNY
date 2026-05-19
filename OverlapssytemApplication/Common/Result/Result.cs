using OverlapssytemApplication.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Common.Result
{
    // Result-klassen er en generisk wrapper, der bruges til at repræsentere resultatet af en operation, som enten kan være en succes eller en fejl.
    // Den indeholder information om, hvorvidt operationen var vellykket, og hvis ikke, hvilken fejl der opstod. 
    public class Result
    {
        public bool Success { get; }
        public Error? Error { get; }

        protected Result(bool success, Error? error)
        {
            Success = success;
            Error = error;
        }

        
        public static Result Ok() => new(true, null);

        public static Result Fail(Error error) => new(false, error);

        // Implicit conversions - gør at man bare kan returnere en Error i stedet for at skulle skrive Result.Fail(error)
        public static implicit operator Result(Error error)
        => Fail(error);

        // Helper 
        public static Result<T> Ok<T>(T value) => Result<T>.Ok(value);
        public static Result<T> Fail<T>(Error error) => Result<T>.Fail(error);

        public TResult Match<TResult>(
        Func<TResult> onSuccess,
        Func<Error, TResult> onFailure)
        {
            return Success
                ? onSuccess()
                : onFailure(Error);
        }
    }

    //Result<T> udvider Result-klassen og indeholder også den faktiske værdi(af typen T) i tilfælde af en succes.
    //Bruges til metoder der returnerer en værdi.
    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value, bool success, Error? error)
            : base(success, error)
        {
            Value = value;
        }

        public static Result<T> Ok(T value)
            => new(value, true, null);

        public new static Result<T> Fail(Error error)
            => new(default, false, error);

        // Implicit conversions - gør at man bare kan returnere en T eller en Error i stedet for at skulle skrive Result.Ok(value) eller Result.Fail(error)
        public static implicit operator Result<T>(T value)
            => Ok(value);

        public static implicit operator Result<T>(Error error)
            => Fail(error);


        //Bruges til at håndtere både success og failure - reducerer if-else statements (mindre boilerplate code)
        //Bruges i controllers via ApiControllerBase 

        public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<Error, TResult> onFailure)
        {
            return Success
                ? onSuccess(Value)
                : onFailure(Error);
        }

    }
}

