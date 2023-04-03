using TheStore.SharedModels.Models;

namespace TheStore.SharedModels.Models.Category
{
	public class UpdateRequest : RequestBase
	{
		public override string Route => "categories";
	}
}
