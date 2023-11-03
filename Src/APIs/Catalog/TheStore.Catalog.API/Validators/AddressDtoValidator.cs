using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class AddressDtoValidator : AbstractValidator<AddressDto>
	{
		public AddressDtoValidator()
		{
			RuleFor(x => x.Country)
				.NotEmpty();

			RuleFor(x => x.City)
				.NotEmpty();

			RuleFor(x => x.Street)
				.NotEmpty();

			RuleFor(x => x.ZipCode)
				.NotEmpty();

			RuleFor(x => x.Coordinate)
				.NotEmpty()
				.SetValidator(ModelValidators.CoordinateDtoValidator);
		}
	}
}
