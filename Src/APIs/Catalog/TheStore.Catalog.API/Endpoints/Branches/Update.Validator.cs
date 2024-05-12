using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.BranchId)
				.NotEmpty();

			RuleFor(x => x.Branch)
				.NotEmpty()
				.SetValidator(ModelValidators.BranchDtoUpdateValidator);
		}
	}
}