using Grpc.Core;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Branches;

namespace TheStore.Catalog.API.Endpoints
{
	public class EntityCheck : EntityChecks.EntityChecksBase
	{
		private readonly IReadApiRepository<CatalogDbContext, Category> categoryRepository;
		private readonly IReadApiRepository<CatalogDbContext, Branch> branchRepository;
		private readonly IReadApiRepository<CatalogDbContext, Product> productRepository;

		public EntityCheck(
			IReadApiRepository<CatalogDbContext, Category> categoryRepository,
			IReadApiRepository<CatalogDbContext, Branch> branchRepository,
			IReadApiRepository<CatalogDbContext, Product> productRepository,
		{
			this.categoryRepository = categoryRepository;
			this.branchRepository = branchRepository;
			this.productRepository = productRepository;
		}

		public override async Task<CheckReply> BranchExists(CheckRequest request, ServerCallContext context)
		{
			var exists = await branchRepository
				.AnyAsync(new CheckBranchExistsReadSpec(request.Id));

			return new CheckReply()
			{
				Result = exists
			};
		}

		public async override Task<CheckReply> CategoryExists(CheckRequest request, ServerCallContext context)
		{
			var exists = await categoryRepository
				.AnyAsync(new CheckCategoryExistsReadSpec(new CategoryId(request.Id)));

			return new CheckReply()
			{
				Result = exists
			};
		}

		public async override Task<CheckReply> ProductExists(CheckRequest request, ServerCallContext context)
		{
			var exists = await productRepository
				.AnyAsync(new CheckProductExistsReadSpec(new ProductId(request.Id)));

			return new CheckReply()
			{
				Result = exists
			};
		}
	}
}