using FluentValidation;
using TheStore.Requests.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class DeleteValidator : AbstractValidator<DeleteRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.BranchId)
				.NotEmpty();
		}
	}
}