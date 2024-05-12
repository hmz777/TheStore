using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class GetByIdValidator : AbstractValidator<GetByIdRequest>
	{
		public GetByIdValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}
