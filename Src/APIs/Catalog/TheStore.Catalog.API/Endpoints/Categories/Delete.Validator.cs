using FluentValidation;
using TheStore.Requests.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class DeleteValidator : AbstractValidator<DeleteRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();
		}
	}
}
