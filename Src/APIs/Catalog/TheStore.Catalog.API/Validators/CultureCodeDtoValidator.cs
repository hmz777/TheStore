using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class CultureCodeDtoValidator : AbstractValidator<CultureCodeDto>
	{
		public CultureCodeDtoValidator()
		{
			RuleFor(x => x.Code)
				.NotEmpty();
		}
	}
}