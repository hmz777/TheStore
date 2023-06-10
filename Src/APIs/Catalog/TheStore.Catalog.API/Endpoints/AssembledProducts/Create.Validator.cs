using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class CreateValidator : AbstractValidator<CreateAssembledRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();

			RuleFor(x => x.Name)
				.NotEmpty();

			RuleFor(x => x.Description)
				.NotEmpty();

			RuleFor(x => x.ShortDescription)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();
		}
	}
}
