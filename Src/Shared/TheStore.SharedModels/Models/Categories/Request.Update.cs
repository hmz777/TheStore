using Microsoft.AspNetCore.Mvc;

namespace TheStore.SharedModels.Models.Categories
{
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
	}
}
