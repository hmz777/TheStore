using System.ComponentModel;

namespace TheStore.Blazor.Models.Categories
{
	[DisplayName("Category." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }

		public DeleteRequest()
		{

		}

		public DeleteRequest(int categoryId)
		{
			CategoryId = categoryId;
		}
	}
}
