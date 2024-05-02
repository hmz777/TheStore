using System.ComponentModel;

namespace TheStore.Web.Models.Products
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