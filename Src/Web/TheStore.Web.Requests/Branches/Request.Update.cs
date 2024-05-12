using System.ComponentModel;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Web.Requests.Branches
{
	[DisplayName("Branch." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }

		public BranchDtoUpdate Branch { get; set; }
	}
}