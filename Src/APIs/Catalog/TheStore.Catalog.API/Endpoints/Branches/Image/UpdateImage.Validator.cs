using FluentValidation;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches.Image
{
	public class UpdateImageValidator : AbstractValidator<UpdateImageRequest>
	{
		public UpdateImageValidator()
		{
			RuleFor(x => x.BranchId)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.ChildRules(x =>
				{
					x.RuleFor(y => y.Alt)
					 .NotEmpty();

					x.RuleFor(y => y.File)
					 .NotEmpty();
				});
		}
	}
}