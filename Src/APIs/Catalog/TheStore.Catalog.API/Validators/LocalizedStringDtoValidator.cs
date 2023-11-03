using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class LocalizedStringDtoValidator : AbstractValidator<LocalizedStringDto>
	{
		public LocalizedStringDtoValidator()
		{
			RuleFor(x => x.Value)
				.NotEmpty();

			RuleFor(x => x.CultureCode)
				.NotEmpty()
				.SetValidator(ModelValidators.CultureCodeDtoValidator);
		}
	}
}
