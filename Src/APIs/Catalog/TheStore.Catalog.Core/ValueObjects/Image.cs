using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		[NotMapped]
		public Uri FileUri { get; }
		public string StringFileUri { get; }
		public string FileNameWithExtension => Path.GetExtension(FileUri.AbsoluteUri);
		public string Alt { get; }

		// Ef Core
		public Image()
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

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return FileUri;
		}
	}
}