using TheStore.Catalog.API.Endpoints;

namespace TheStore.Catalog.API.Helpers
{
	public static class PipelineExtensions
	{
		public static void MapGrpcServices(this WebApplication webApplication)
		{
			webApplication.MapGrpcService<EntityCheck>();
		}
	}
}