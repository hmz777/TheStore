using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		[FromRoute(Name = nameof(CategoryId))]
		public int CategoryId { get; set; }

		[FromBody]
		public int Order { get; set; }

		[FromBody]
		public MultilanguageStringDto Name { get; set; }

		[FromBody]
		public bool Published { get; set; }

		public UpdateRequest(int categoryId, int order, MultilanguageStringDto name, bool published)
		{
			CategoryId = categoryId;
			Order = order;
			Name = name;
			Published = published;
		}
	}
}