using Ardalis.GuardClauses;
using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.BlazorApp.Client.Mediator.Queries
{
    public class ListProductReviewsQuery : IRequest<Result<ProductReviewsPaginatedResult>>
    {
        public string Identifier { get; }
        public int Take { get; }
        public int Page { get; }

        public ListProductReviewsQuery(string identifier, int take, int page)
        {
            Guard.Against.NullOrEmpty(identifier, nameof(identifier));
            Guard.Against.NegativeOrZero(take, nameof(take));
            Guard.Against.NegativeOrZero(page, nameof(page));

            Identifier = identifier;
            Take = take;
            Page = page;
        }
    }
}