using FluentValidation;
using TheStore.Requests.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class ListValidator : AbstractValidator<ListRequest>
	{
		public ListValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);

			RuleFor(x => x.Take)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);
		}
	}
}
