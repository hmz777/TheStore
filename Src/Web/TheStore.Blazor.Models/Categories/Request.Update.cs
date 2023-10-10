using System.ComponentModel;

namespace TheStore.Blazor.Models.Categories
{
	[DisplayName("Category." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }

		public int Order { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }

		public UpdateRequest()
		{

		}

		public UpdateRequest(int categoryId, int order, string name, bool active)
		{
			CategoryId = categoryId;
			Order = order;
			Name = name;
			Active = active;
		}
	}
}
