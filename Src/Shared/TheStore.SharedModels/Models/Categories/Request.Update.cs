using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		[FromRoute]
		public int CategoryId { get; set; }

		[FromBody]
		public int Order { get; set; }

		[FromBody]
		public string Name { get; set; }

		[FromBody]
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
