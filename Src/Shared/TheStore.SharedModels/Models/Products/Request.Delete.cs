using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
	}
}
