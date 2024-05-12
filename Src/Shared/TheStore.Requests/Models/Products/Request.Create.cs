using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Requests.Models.Products
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