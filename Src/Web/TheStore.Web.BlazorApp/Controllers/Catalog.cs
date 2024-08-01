using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;

namespace TheStore.Web.BlazorApp.Controllers
{
    [ApiController]
    public class Catalog(IMediator mediator) : ControllerBase
    {
        // TODO: Do logging here

        [Route(ListRequest.RouteTemplate)]
        [HttpGet]
        public async Task<Result<ProductsPaginatedResult>> QueryProductCatalog([Required] int page = 1, [Required] int take = 1)
        {
            if (page < 1)
                page = 1;

            if (take < 1)
                take = 1;

            return await mediator.Send(new ListProductsWithPaginationQuery(page, take));
        }

        [Route(GetByIdentifierRequest.RouteTemplate)]
        [HttpGet]
        public async Task<Result<ProductDetailsDtoRead>> GetProductDetails([Required] string identifier)
        {
            return await mediator.Send(new GetProductDetailsQuery(identifier));
        }

        [Route(ListReviewsRequest.RouteTemplate)]
        [HttpGet]
        public async Task<Result<ProductReviewsPaginatedResult>> QueryProductReviews(
            [Required] string identifier, [Required] int page = 1, [Required] int take = 1)
        {
            if (page < 1)
                page = 1;

            if (take < 1)
                take = 1;

            return await mediator.Send(new ListProductReviewsQuery(identifier, take, page));
        }
    }
}