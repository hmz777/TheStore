using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		public int Order { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }
	}
}