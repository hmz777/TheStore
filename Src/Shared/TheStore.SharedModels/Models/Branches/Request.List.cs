using System.ComponentModel;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "branches";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}