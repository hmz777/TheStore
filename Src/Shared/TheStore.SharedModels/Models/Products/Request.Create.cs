using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "products";
		public override string Route => RouteTemplate;

		[FromBody]
		public ProductDtoUpdate Product { get; set; }
	}
}