using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "Categories.Id";
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }

		public GetByIdRequest()
		{

		}

		public GetByIdRequest(int categoryId)
		{
			CategoryId = categoryId;
		}
	}
}