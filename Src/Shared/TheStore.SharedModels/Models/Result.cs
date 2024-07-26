
namespace TheStore.SharedModels.Models
{
    public class Result<T>
    {
        public T Value { get; }
        public Status Status { get; }
        public string Message { get; }
        public bool IsSuccessful => Status == Status.Success;

        public Result(T value, Status status, string message)
        {
            Value = value;
            Status = status;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }

    public class Result
    {
        public Status Status { get; }
        public string Message { get; }
        public bool IsSuccessful => Status == Status.Success;

        public Result(Status status, string message)
        {
            Status = status;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}