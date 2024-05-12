using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace TheStore.Catalog.API.Validators
{
	public class FileUploadValidator : AbstractValidator<IBrowserFile>
	{
		// TODO: For starters, file settings are hardcoded but later we'll grab them from the database
		private readonly string[] allowedExtensions = [".jpg", ".png"];
		private readonly long allowedSize = 50000000;

		public FileUploadValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.Must(x =>
				{
					foreach (var ext in allowedExtensions)
					{
						if (Path.GetExtension(x) == ext)
						{
							return true;
						}
					}

					return false;
				});

			RuleFor(x => x.Size)
				.LessThanOrEqualTo(allowedSize);
		}
	}
}