using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Branches
{
	[DisplayName("Branch." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }
	}
}