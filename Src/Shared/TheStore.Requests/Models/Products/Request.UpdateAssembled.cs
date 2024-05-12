using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(UpdateAssembledRequest))]
	public class UpdateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public AssembledProductDtoUpdate AssembledProduct { get; set; }
	}
}