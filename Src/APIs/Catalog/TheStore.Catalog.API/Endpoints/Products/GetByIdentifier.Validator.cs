using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class GetByIdValidator : AbstractValidator<GetByIdentifierRequest>
	{
		public GetByIdValidator()
		{
			RuleFor(x => x.Identifier)
				.NotEmpty();
		}
	}
}
