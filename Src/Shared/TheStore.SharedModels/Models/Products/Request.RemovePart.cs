namespace TheStore.SharedModels.Models.Products
{
	public class RemovePartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
		public int PartId { get; set; }
	}
}