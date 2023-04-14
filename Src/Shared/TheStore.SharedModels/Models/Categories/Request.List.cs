using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "categories";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}
