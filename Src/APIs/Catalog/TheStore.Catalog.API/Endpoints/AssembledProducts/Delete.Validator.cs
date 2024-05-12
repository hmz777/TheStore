using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class DeleteValidator : AbstractValidator<DeleteAssembledRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}
