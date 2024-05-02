using System.Net.Http.Json;

namespace TheStore.Web.BlazorApp.Client.Services.Configuration
{
	public class AppConfigurationService
	{
		private readonly HttpClient httpClient;

		public AppConfiguration? AppConfiguration { get; private set; }

		public bool AppConfigured => AppConfiguration != null;

		public AppConfigurationService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task RequestAppConfiguration()
		{
			AppConfiguration = await httpClient.GetFromJsonAsync<AppConfiguration>("config.hmz.com");
		}
	}
}
