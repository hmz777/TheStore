using Microsoft.AspNetCore.Mvc;

namespace TheStore.SharedModels.Models.Category
{
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		[FromBody]
		public int Order { get; set; }

		[FromBody]
		public string Name { get; set; }

		[FromBody]
		public bool Active { get; set; }
	}
}