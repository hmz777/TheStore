using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public ProductDtoUpdate Product { get; set; }
	}
}