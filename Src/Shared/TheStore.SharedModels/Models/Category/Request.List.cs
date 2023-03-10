using TheStore.SharedModels.Models;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "Categories";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}
