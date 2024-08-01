using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class ListProductsWithPaginationHandler(CatalogService catalogService) :
        IRequestHandler<ListProductsWithPaginationQuery, Result<ProductsPaginatedResult>>
    {
        public async Task<Result<ProductsPaginatedResult>> Handle(
            ListProductsWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            return await catalogService.ListProductsPaginated(request.Take, request.Page, cancellationToken);
        }
    }
}