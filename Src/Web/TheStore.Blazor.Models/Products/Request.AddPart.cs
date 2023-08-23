using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Products
{
	public class AddPartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public int PartId { get; set; }
	}
}