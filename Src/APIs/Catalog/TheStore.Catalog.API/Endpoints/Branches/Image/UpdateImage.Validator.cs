using FluentValidation;
using TheStore.Catalog.API.Validators;
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
                .SetValidator(x => new ImageValidator());
        }
    }
}