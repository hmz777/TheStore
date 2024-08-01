using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class GetProductDetailsHandler(CatalogService catalogService) :
        IRequestHandler<GetProductDetailsQuery, Result<ProductDetailsDtoRead>>
    {
        public async Task<Result<ProductDetailsDtoRead>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            return await catalogService.GetProductDetails(request.Identifier, cancellationToken);
        }
    }
}