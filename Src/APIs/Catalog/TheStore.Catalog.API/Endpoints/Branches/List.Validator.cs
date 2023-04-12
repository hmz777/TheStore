using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class ListValidator : AbstractValidator<ListRequest>
	{
		public ListValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty();

			RuleFor(x => x.Take)
				.NotEmpty();
		}
	}
}
