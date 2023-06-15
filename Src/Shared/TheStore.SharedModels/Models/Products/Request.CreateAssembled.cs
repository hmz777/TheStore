using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(CreateAssembledRequest))]
	public class CreateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts";
		public override string Route => RouteTemplate;

		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }

		public CreateAssembledRequest()
		{

		}

		public CreateAssembledRequest(int categoryId, string name, string description, string shortDescription, string sku)
		{
			CategoryId = categoryId;
			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
		}
	}
}
