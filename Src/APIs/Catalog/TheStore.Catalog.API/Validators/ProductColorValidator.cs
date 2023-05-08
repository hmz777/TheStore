using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class AddProductColorDtoValidator : AbstractValidator<AddProductColorDto>
	{
		public AddProductColorDtoValidator()
		{
			RuleFor(x => x.ColorCode)
				.NotEmpty();
		}
	}

	public class UpdatedProductColorDtoValidator : AbstractValidator<UpdateProductColorDto>
	{
		public UpdatedProductColorDtoValidator()
		{
			RuleFor(x => x.ProductColorId)
				.NotEmpty();

			RuleFor(x => x.ColorCode)
				.NotEmpty();
		}
	}
}