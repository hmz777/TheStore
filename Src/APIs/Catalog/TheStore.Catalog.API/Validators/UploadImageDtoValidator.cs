using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class UploadImageDtoValidator : AbstractValidator<UploadImageDto>
	{
		public UploadImageDtoValidator()
		{
			RuleFor(x => x.File)
				.NotEmpty()
				.SetValidator(ModelValidators.FileUploadValidator);

			RuleFor(x => x.Alt)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);
		}
	}
}