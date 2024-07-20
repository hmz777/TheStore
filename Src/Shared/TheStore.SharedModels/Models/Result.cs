
namespace TheStore.SharedModels.Models
{
	public class Result
	{
		public Status Status { get; }
		public string Message { get; }

		public Result(Status status, string message)
		{
			Status = status;
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}
	}
}
