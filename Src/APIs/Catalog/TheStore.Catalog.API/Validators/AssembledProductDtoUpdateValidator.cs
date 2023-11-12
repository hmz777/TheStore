using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Validators
{
	public class AssembledProductDtoUpdateValidator : AbstractValidator<AssembledProductDtoUpdate>
	{
        public AssembledProductDtoUpdateValidator()
        {
			RuleFor(x => x.CategoryId)
				.NotEmpty();

			RuleFor(x => x.Name)
				 .NotEmpty();

			RuleFor(x => x.ShortDescription)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);

			RuleFor(x => x.Description)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);
		}
    }
}
