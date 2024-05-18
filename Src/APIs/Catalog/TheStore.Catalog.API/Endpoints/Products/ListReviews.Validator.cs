using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class ListReviewsValidator : AbstractValidator<ListReviewsRequest>
	{
		public ListReviewsValidator()
		{
			RuleFor(r => r.ProductId)
				.NotEmpty();
		}
	}
}
