using TheStore.SharedModels.Models;

namespace TheStore.SharedModels.Models.Category
{
	public class GetByIdRequest : RequestBase
	{
		public const string RouteTemplate = "Categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }
	}
}