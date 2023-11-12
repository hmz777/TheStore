using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(CreateAssembledRequest))]
	public class CreateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts";
		public override string Route => RouteTemplate;

		[FromBody]
		public AssembledProductDtoUpdate AssembledProduct { get; set; }
	}
}
