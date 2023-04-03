using TheStore.SharedModels.Models;

namespace TheStore.SharedModels.Models.Category
{
	public class DeleteRequest : RequestBase
	{
		public override string Route => "categories";
	}
}
