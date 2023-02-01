using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		public Uri FileUri { get; }
		public string FileNameWithExtension => Path.GetExtension(FileUri.AbsoluteUri);
		public string Alt { get; }

		public Image(Uri fileUri, string alt)
		{
			Guard.Against.Null(fileUri, nameof(fileUri));
			Guard.Against.NullOrEmpty(alt, nameof(alt));
			Guard.Against.InvalidInput(fileUri, nameof(fileUri), x => fileUri.IsFile || Path.HasExtension(fileUri.LocalPath), message: "Uri is not a file");

			FileUri = fileUri;
			Alt = alt;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return FileUri;
		}
	}
}