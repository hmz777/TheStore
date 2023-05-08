using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.BranchId)
				.NotEmpty();

			RuleFor(x => x.Name)
				.NotEmpty();

			RuleFor(x => x.Description)
				.NotEmpty();

			RuleFor(x => x.Address)
				.NotEmpty()
				.SetValidator(x => new AddressValidator());
		}
	}
}
