using Microsoft.AspNetCore.Mvc;

namespace ProductivityBook.API.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsFailure => !IsSuccess;

        public static Result Success() => new Result(true, null);

        public static Result Failure(string? error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(bool isSuccess, T value, string? error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);

        public static new Result<T> Failure(string? error) => new Result<T>(false, default!, error);

        public static NotFoundResult<T> NotFound(string? message) => new NotFoundResult<T>(message);
    }

    public class NotFoundResult<T> : Result<T>
    {
        public NotFoundResult(string? error) : base(false, default!, error) { }
    }
}
