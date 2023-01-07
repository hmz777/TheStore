using System.Collections.Generic;

namespace TheStore.Web.Services.StatusMessage;

public interface IStatusMessageService
{
	void AddMessage(string message, MessageType messageType);
	IEnumerable<StatusMessage> GetMessages();
}