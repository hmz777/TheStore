using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class MultilanguageStringDtoValidator : AbstractValidator<MultilanguageStringDto>
	{
		public MultilanguageStringDtoValidator()
		{
			RuleFor(x => x.LocalizedStrings)
				.NotEmpty();

			RuleForEach(x => x.LocalizedStrings)
				.NotEmpty()
				.SetValidator(ModelValidators.LocalizedStringDtoValidator);
		}
	}
}