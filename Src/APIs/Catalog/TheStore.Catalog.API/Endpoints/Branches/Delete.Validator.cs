using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class DeleteValidator : AbstractValidator<DeleteRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.BrancheId)
				.NotEmpty();
		}
	}
}