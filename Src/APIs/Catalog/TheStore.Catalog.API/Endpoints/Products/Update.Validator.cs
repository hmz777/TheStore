using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Product)
				.NotEmpty()
				.SetValidator(ModelValidators.ProductDtoUpdateValidator);
		}
	}
}