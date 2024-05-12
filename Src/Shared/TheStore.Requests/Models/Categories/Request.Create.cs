using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Requests.Models.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		[FromBody]
		public CategoryDtoUpdate Category { get; set; }
	}
}