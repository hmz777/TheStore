using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class ImageValidator : AbstractValidator<UpdateImageDto>
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