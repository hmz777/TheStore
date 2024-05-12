using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class CreateValidator : AbstractValidator<CreateAssembledRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.AssembledProduct)
				.NotEmpty()
				.SetValidator(ModelValidators.AssembledProductDtoUpdateValidator);
		}
	}
}
