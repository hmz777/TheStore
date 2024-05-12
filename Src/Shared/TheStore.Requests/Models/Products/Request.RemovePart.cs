using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(RemovePartRequest))]
	public class RemovePartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts/{PartId:int}/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{PartId:int}", PartId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(PartId))]
		public int PartId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }
	}
}