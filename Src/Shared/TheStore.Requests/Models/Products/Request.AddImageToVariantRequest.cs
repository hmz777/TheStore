using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(AddImageToVariantRequest))]
	public class AddImageToVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}/images";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku);

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		[FromForm]
		public UploadImageDto Image { get; set; }
	}
}