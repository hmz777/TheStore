using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class ImageValidator : AbstractValidator<ImageDto>
	{
		public ImageValidator()
		{
			RuleFor(x => x.File)
				.NotEmpty();

			RuleFor(x => x.Alt)
				.NotEmpty();
		}
	}
}