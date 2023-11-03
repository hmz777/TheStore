using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromBody]
		public int CategoryId { get; set; }

		[FromBody]
		public string Name { get; set; }

		public UpdateRequest(int productId, int categoryId, string name)
		{
			ProductId = productId;
			CategoryId = categoryId;
			Name = name;
		}
	}
}