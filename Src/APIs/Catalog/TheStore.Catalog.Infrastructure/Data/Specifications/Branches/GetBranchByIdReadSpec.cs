using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Branches;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Branches
{
	public class GetBranchByIdReadSpec : Specification<Branch>, ISingleResultSpecification
	{
		public GetBranchByIdReadSpec(int branchId)
		{
			Query
				.Where(b => b.Id == branchId)
				.AsNoTracking();
		}
	}
}