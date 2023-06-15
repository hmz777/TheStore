using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		[NotMapped]
		public Uri FileUri { get; }
		public string StringFileUri { get; private set; }

		[NotMapped]
		public string FileNameWithExtension => Path.GetFileName(FileUri.AbsoluteUri);
		public string Alt { get; private set; }

		// Ef Core
		private Image()
		{

		}

		public Image(string stringFileUri, string alt)
		{
			Guard.Against.Null(stringFileUri, nameof(stringFileUri));
			Guard.Against.NullOrEmpty(alt, nameof(alt));

			FileUri = new Uri(stringFileUri, UriKind.RelativeOrAbsolute);

			StringFileUri = stringFileUri;
			Alt = alt;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return StringFileUri;
		}
	}
}