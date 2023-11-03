using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class CurrencyDtoValidator : AbstractValidator<CurrencyDto>
	{
        public CurrencyDtoValidator()
        {
            RuleFor(x => x.CurrencyCode)
                .NotEmpty();
        }
    }
}