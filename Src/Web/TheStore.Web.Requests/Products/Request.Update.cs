using System.ComponentModel;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public ProductDtoUpdate Product { get; set; }
	}
}