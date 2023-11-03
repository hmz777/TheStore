using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		public int Order { get; set; }

		public MultilanguageStringDto Name { get; set; }

		public bool Active { get; set; }

		public CreateRequest(int order, MultilanguageStringDto name, bool active)
		{
			Order = order;
			Name = name;
			Active = active;
		}
	}
}