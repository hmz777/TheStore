using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.Branches
{
	[DisplayName("Branch." + nameof(DeleteRequest))]
	public class DeleteRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }
	}
}