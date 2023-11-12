using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(AddPartRequest))]
	public class AddPartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public int PartId { get; set; }

		[FromBody]
		public string Sku { get; set; }
	}
}