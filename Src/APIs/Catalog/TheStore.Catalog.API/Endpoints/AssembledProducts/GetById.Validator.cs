using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class GetByIdValidator : AbstractValidator<GetAssembledByIdRequest>
	{
		public GetByIdValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}
