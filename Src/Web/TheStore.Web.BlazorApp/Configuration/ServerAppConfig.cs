namespace TheStore.Web.BlazorApp.Configuration
{
	public class ServerAppConfig
	{
		public const string Key = "Apis";
		public List<Endpoint> Endpoints { get; set; } = [];
	}
}