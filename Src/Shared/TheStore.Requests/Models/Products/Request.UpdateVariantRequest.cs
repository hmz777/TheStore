using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(UpdateVariantRequest))]
	public class UpdateVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku);

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		[FromBody]
		public ProductVariantDtoUpdate Variant { get; set; }
	}
}