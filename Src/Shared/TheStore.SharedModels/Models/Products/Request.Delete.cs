namespace TheStore.SharedModels.Models.Products
{
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
	}
}
