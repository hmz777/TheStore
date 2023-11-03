using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class MoneyDtoValidator : AbstractValidator<MoneyDto>
	{
        public MoneyDtoValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();

            RuleFor(x => x.Currency)
                .SetValidator(ModelValidators.CurrencyDtoValidator);
        }
    }
}