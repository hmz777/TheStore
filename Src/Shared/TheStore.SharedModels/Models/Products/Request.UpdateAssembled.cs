using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(UpdateAssembledRequest))]
	public class UpdateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		internal override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
	}
}