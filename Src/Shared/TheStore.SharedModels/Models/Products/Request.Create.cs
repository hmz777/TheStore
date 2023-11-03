using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "products";
		public override string Route => RouteTemplate;

		public int CategoryId { get; set; }
		public string Name { get; set; }

		public CreateRequest(int categoryId, string name)
		{
			CategoryId = categoryId;
			Name = name;
		}
	}
}