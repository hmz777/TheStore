using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.Product)
				.NotEmpty()
				.SetValidator(ModelValidators.ProductDtoUpdateValidator);
		}
	}
}
