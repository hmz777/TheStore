using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class AddImageDtoValidator : AbstractValidator<AddImageDto>
	{
		public AddImageDtoValidator()
		{
			RuleFor(x => x.File)
				.NotEmpty();

			RuleFor(x => x.Alt)
				.NotEmpty();
		}
	}

	public class UpdateImageDtoValidator : AbstractValidator<UpdateImageDto>
	{
		public UpdateImageDtoValidator()
		{
			RuleFor(x => x.File)
				.NotEmpty();

			RuleFor(x => x.Alt)
				.NotEmpty();
		}
	}
}