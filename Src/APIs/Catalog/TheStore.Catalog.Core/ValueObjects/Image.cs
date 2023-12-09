using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		[NotMapped]
		public Uri FileUri { get; }

		[NotMapped]
		public string FileNameWithExtension =>
			Path.GetFileName(FileUri.ToString());

		public string StringFileUri { get; }

		public MultilanguageString Alt { get; }

		public bool IsMainImage { get; }

		// Ef Core
		private Image() { }

		public Image(string stringFileUri, MultilanguageString alt, bool isMainImage)
		{
			Guard.Against.Null(stringFileUri, nameof(stringFileUri));
			Guard.Against.Null(alt, nameof(alt));

			FileUri = new Uri(stringFileUri, UriKind.RelativeOrAbsolute);

			StringFileUri = stringFileUri;
			Alt = alt;
			IsMainImage = isMainImage;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return StringFileUri;
		}
	}
}