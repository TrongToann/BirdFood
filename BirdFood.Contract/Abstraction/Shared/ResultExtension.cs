namespace BirdFood.Contract.Abstraction.Shared
{
    public static class ResultExtension
    {
        public static Result<T> Ensure<T>(
            this Result<T> result,
            Func<T, bool> predicate,
            Error error) 
        {
            if (result.IsFailure)
            {
                return result;
            }
            return predicate(result.Value) ? 
                result :
                Result.Failure<T>(error);
        }
        public static Result<TOut> Map<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> mappingFunc)
        {
            return result.IsSuccess ?
                Result.Success(mappingFunc(result.Value)) :
                Result.Failure<TOut>(result.Errors);
        }
        public static Result<T> Ensure<T>(
            T Value,
            Func<T, bool> predicate,
            Error error)
        {
            return predicate(Value) ?
                Result.Success(Value) :
                Result.Failure<T>(error);
        }
        public static Result<T> Combine<T>(params Result<T>[] results)
        {
            if (results.Any(r => r.IsFailure))
            {
                return Result.Failure<T>(
                    results
                    .SelectMany(r => r.Errors)
                    .Distinct()
                    .ToArray());
            }
            return Result.Success(results[0].Value);
        }
        public static Result<(T1, T2)> Combine<T1, T2>(Result<T1> result1, Result<T2> result2)
        {
            if (result1.IsFailure) return Result.Failure<(T1, T2)>(result1.Errors);
            if (result2.IsFailure) return Result.Failure<(T1, T2)>(result2.Errors);
            return Result.Success((result1.Value, result2.Value));
        }
        public static async Task<Result<TOut>> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> func)
        {
            if (result.IsFailure) return Result.Failure<TOut>(result.Errors);
            return await func(result.Value);
        }
        public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
        {
            if (result.IsSuccess) action(result.Value);
            return result;
        }
        public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result, Func<Task> func)
        {
            if (result.IsSuccess) await func();
            return result;
        }
        public static async Task<Result<TIn>> Tap<TIn>(
            this Task<Result<TIn>> resultTask, Func<TIn, Task> func)
        {
            Result<TIn> result = await resultTask;
            if (result.IsSuccess) await func(result.Value);
            return result;
        }
    }
}
