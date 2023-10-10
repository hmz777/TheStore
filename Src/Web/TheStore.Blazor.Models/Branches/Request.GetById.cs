using System.ComponentModel;

namespace TheStore.Blazor.Models.Branches
{
	[DisplayName("Branch." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "Branches.Id";
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }

		public GetByIdRequest()
		{

		}

		public GetByIdRequest(int branchId)
		{
			BranchId = branchId;
		}
	}
}