using FluentValidation;
using TheStore.SharedModels.Models.Category;

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
