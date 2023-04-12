using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class GetByIdValidator : AbstractValidator<GetByIdRequest>
	{
		public GetByIdValidator()
		{
			RuleFor(x => x.BranchId)
				.NotEmpty();
		}
	}
}