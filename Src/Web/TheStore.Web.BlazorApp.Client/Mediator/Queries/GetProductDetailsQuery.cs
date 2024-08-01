using Ardalis.GuardClauses;
using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.BlazorApp.Client.Mediator.Queries
{
    public class GetProductDetailsQuery : IRequest<Result<ProductDetailsDtoRead>>
    {
        public string Identifier { get; }

        public GetProductDetailsQuery(string identifier)
        {
            Guard.Against.NullOrEmpty(identifier, nameof(identifier));

            Identifier = identifier;
        }
    }
}
