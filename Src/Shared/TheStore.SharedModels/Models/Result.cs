
using Ardalis.GuardClauses;
using System.Text.Json.Serialization;

namespace TheStore.SharedModels.Models
{
    public class Result<T> : Result
    {
        [JsonInclude]
        public T? Value { get; private set; }

        [JsonConstructor]
        private Result() : base() { }

        public Result(T value, Status status, string message) : base(status, message)
        {
            Guard.Against.Null(value, nameof(value));

            Value = value;
        }

        public Result(T value, Status status) : base(status)
        {
            Guard.Against.Null(value, nameof(value));

            Value = value;
        }
    }

    public class Result
    {
        [JsonInclude]
        public Status Status { get; private set; }

        [JsonInclude]
        public string? Message { get; private set; }

        [JsonInclude]
        public bool IsSuccessful => Status == Status.Success;

        [JsonConstructor]
        protected Result() { }

        public Result(Status status, string message)
        {
            Status = status;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public Result(Status status)
        {
            Status = status;
        }

        public override string ToString() => $"({Status}) {Message}";

        public static Result Failure(string message) => new(Status.Failure, message);
        public static Result Success(string message) => new(Status.Success, message);

        public static Result<T> Failure<T>(T value, string message) => new(value, Status.Failure, message);
        public static Result<T> Success<T>(T value) => new(value, Status.Success);
    }
}