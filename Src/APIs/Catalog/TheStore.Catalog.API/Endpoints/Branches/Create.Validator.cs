using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.Branch)
				.NotEmpty()
				.SetValidator(x => ModelValidators.BranchDtoUpdateValidator);
		}
	}
}