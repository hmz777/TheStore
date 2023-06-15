using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace TheStore.Web.Services.StatusMessage;

public class StatusMessageService : IStatusMessageService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

	private const string StatusMessagesKey = "StatusMessages";
	private readonly List<StatusMessage> messages = new();

	public StatusMessageService(
		IHttpContextAccessor httpContextAccessor,
		ITempDataDictionaryFactory tempDataDictionaryFactory)
	{
		_httpContextAccessor = httpContextAccessor;
		_tempDataDictionaryFactory = tempDataDictionaryFactory;
	}

	public void AddMessage(string message, MessageType messageType)
	{
		Guard.Against.NullOrEmpty(message, nameof(message));
		Guard.Against.EnumOutOfRange(messageType, nameof(messageType));

		var msg = new StatusMessage(message, messageType);

		messages.Add(msg);

		var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);

		tempData[StatusMessagesKey] = JsonSerializer.Serialize(messages);
	}

	public IEnumerable<StatusMessage> GetMessages()
	{
		var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);

		tempData.TryGetValue(StatusMessagesKey, out object? o);

		return o == null ? new List<StatusMessage>() : JsonSerializer.Deserialize<List<StatusMessage>>((string)o) ?? new List<StatusMessage>();
	}
}