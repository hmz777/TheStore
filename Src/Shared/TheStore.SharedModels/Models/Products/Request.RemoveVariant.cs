using Microsoft.AspNetCore.Mvc;

namespace TheStore.SharedModels.Models.Products
{
	public class RemoveVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}";

		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku);

		public int ProductId { get; set; }

		public string Sku { get; set; }
	}
}
