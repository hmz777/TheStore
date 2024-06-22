using Ardalis.GuardClauses;
using MediatR;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.BlazorApp.Client.Mediator.Queries
{
	public class ListProductsWithPaginationQuery : IRequest<List<ProductCatalogDtoRead>>
	{
		public int Page { get; }
		public int Take { get; }

		public ListProductsWithPaginationQuery(int page, int take)
		{
			Guard.Against.NegativeOrZero(page, nameof(page));
			Guard.Against.NegativeOrZero(take, nameof(take));

			Page = page;
			Take = take;
		}
	}
}
