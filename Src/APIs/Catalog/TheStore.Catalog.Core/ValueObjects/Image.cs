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

		public Image(string fileUri, string alt)
		{
			Guard.Against.Null(fileUri, nameof(fileUri));
			Guard.Against.NullOrEmpty(alt, nameof(alt));

			FileUri = new Uri(fileUri);

			Guard.Against.InvalidInput(FileUri, nameof(FileUri), x => FileUri.IsFile || Path.HasExtension(FileUri.LocalPath), message: "Uri is not a file");

			StringFileUri = fileUri;
			Alt = alt;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return StringFileUri;
			yield return Alt;
		}
	}
}