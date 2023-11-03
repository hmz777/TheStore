using Microsoft.AspNetCore.Mvc;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	public class AddVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public ProductVariantDto ProductVariant { get; set; }
	}
}