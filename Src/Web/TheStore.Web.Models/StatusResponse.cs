namespace TheStore.Web.Models
{
	public class StatusResponse : BaseMessage
	{
		public string Message { get; }
		public StatusResponseType Type { get; }

		public StatusResponse(string message, StatusResponseType type)
		{
			Message = message;
			Type = type;
		}
	}
}
