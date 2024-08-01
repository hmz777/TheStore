using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class ListProductReviewsHandler(CatalogService catalogService) :
        IRequestHandler<ListProductReviewsQuery, Result<ProductReviewsPaginatedResult>>
    {
        public async Task<Result<ProductReviewsPaginatedResult>> Handle(
            ListProductReviewsQuery request,
            CancellationToken cancellationToken)
        {
            return await catalogService.ListProductReviewsPaginated(request.Identifier, request.Take, request.Page, cancellationToken);
        }
    }
}