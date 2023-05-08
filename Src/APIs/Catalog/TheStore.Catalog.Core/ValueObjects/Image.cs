using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

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

			FileUri = new Uri(stringFileUri);

			Guard.Against.InvalidInput(FileUri, nameof(FileUri), x => FileUri.IsFile || Path.HasExtension(FileUri.LocalPath), message: "Uri is not a file");

			StringFileUri = stringFileUri;
			Alt = alt;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Id;
		}
	}
}