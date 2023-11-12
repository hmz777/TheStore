using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Validators
{
	public class BranchDtoUpdateValidator : AbstractValidator<BranchDtoUpdate>
	{
		public BranchDtoUpdateValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty();

			RuleFor(x => x.Description)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);

			RuleFor(x => x.Address)
				.NotEmpty()
				.SetValidator(ModelValidators.AddressDtoValidator);
		}
	}
}
