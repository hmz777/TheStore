using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.MinimumLength(2);
		}
	}
}