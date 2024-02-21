using FluentValidation;

namespace TheStore.Catalog.API.Validators
{
	public class FormFileValidator : AbstractValidator<IFormFile>
	{
		// TODO: For starters, file settings are hardcoded but later we'll grab them from the database
		private string[] allowedExtensions = new[] { ".jpg", ".png" };
		private long allowedSize = 50000000;

		public FormFileValidator()
		{
			RuleFor(x => x.FileName)
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

			RuleFor(x => x.Length)
				.LessThanOrEqualTo(allowedSize);
		}
	}
}
