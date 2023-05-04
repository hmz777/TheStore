using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class AddressValidator : AbstractValidator<AddressDto>
	{
		public AddressValidator()
		{

		}
	}
}
