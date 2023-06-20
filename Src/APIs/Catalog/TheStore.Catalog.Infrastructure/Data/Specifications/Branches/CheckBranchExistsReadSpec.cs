using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Branches;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Branches
{
	public class CheckBranchExistsReadSpec : Specification<Branch>
	{
		public CheckBranchExistsReadSpec(int id)
		{
			Query
				.Where(branch => branch.Id == id)
				.AsNoTracking();
		}
	}
}