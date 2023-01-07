namespace TheStore.Web.Services.StatusMessage;

public class StatusMessage
{
	public string Title => MessageType.ToString();

	public string Message { get; set; }
	public MessageType MessageType { get; set; }

	public StatusMessage(string message, MessageType messageType)
	{
		Message = message;
		MessageType = messageType;
	}
}