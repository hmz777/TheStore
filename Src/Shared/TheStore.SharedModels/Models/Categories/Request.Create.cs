using Microsoft.AspNetCore.Mvc;

namespace TheStore.SharedModels.Models.Categories
{
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		public int Order { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }
	}
}