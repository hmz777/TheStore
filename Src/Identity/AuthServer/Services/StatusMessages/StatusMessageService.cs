using AuthServer.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System.Text.Json;

namespace AuthServer.Services.StatusMessages
{
	public class StatusMessageService
	{
		private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;
		private readonly IStringLocalizer<LocalizationResources> stringLocalizer;
		private readonly IHttpContextAccessor httpContextAccessor;

		public StatusMessageService(
			ITempDataDictionaryFactory tempDataDictionaryFactory,
			IStringLocalizer<LocalizationResources> stringLocalizer,
			IHttpContextAccessor httpContextAccessor)
		{
			this.tempDataDictionaryFactory = tempDataDictionaryFactory;
			this.stringLocalizer = stringLocalizer;
			this.httpContextAccessor = httpContextAccessor;
		}

		public const string TempDataKey = "StatusMessages";

		private List<StatusMessage> Messages { get; set; } = new();

		public void AddMessage(StatusMessageType messageType, string message)
		{
			var tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);

			Messages.Add(new StatusMessage(messageType, stringLocalizer[message]));

			tempData[TempDataKey] = JsonSerializer.Serialize(Messages);
		}
	}

	public static class StatusMessageExtensions
	{
		public static void AsStatusMessages(
			this IEnumerable<IdentityError> identityErrors,
			StatusMessageService statusMessageService)
		{
			foreach (var identityError in identityErrors)
			{
				statusMessageService.AddMessage(StatusMessageType.Error, identityError.Description);
			}
		}

		public static List<StatusMessage> GetStatusMessages(this ITempDataDictionary tempData)
		{
			var messagesExist = tempData.TryGetValue(StatusMessageService.TempDataKey, out object messages);
			var messagesJson = messages as string;

			return messagesExist && messagesJson is not null ?
				JsonSerializer.Deserialize<List<StatusMessage>>(messagesJson) : new List<StatusMessage>();
		}

		public static string GetCssClassName(this StatusMessageType statusMessageType) => statusMessageType switch
		{
			StatusMessageType.Success => "c-alert-success",
			StatusMessageType.Error => "c-alert-danger",
			StatusMessageType.Warning => "c-alert-warning",
			StatusMessageType.Info => "c-alert-info",
			_ => "c-alert-danger"
		};

		public static string GetIconClassName(this StatusMessageType statusMessageType) => statusMessageType switch
		{
			StatusMessageType.Success => "la-check-circle",
			StatusMessageType.Error => "la-times-circle",
			StatusMessageType.Warning => "la-exclamation-circle",
			StatusMessageType.Info => "la-info-circle",
			_ => "la-times-circle"
		};
	}
}