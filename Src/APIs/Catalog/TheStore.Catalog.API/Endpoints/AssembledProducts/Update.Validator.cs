using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class UpdateValidator : AbstractValidator<UpdateAssembledRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.AssembledProduct)
				.NotEmpty()
				.SetValidator(ModelValidators.AssembledProductDtoUpdateValidator);
		}
	}
}