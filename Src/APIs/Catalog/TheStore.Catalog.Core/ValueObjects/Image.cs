using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Image : ValueObject
	{
		public string Uri { get; private set; }
		public string FileName { get; private set; }
		public string Alt { get; private set; }
		public string ColorCode { get; private set; }

		public Image(string uri, string fileName, string alt, string colorCode)
		{
			Guard.Against.NullOrEmpty(uri, nameof(uri));
			Guard.Against.NullOrEmpty(fileName, nameof(fileName));
			Guard.Against.NullOrEmpty(alt, nameof(alt));
			Guard.Against.NullOrEmpty(colorCode, nameof(colorCode));

			if (File.Exists(uri) == false)
			{
				throw new FileNotFoundException($"Image with filename:{fileName} can't be created, {uri} not found");
			}

			Uri = uri;
			FileName = fileName;
			Alt = alt;
			ColorCode = colorCode;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Uri;
		}
	}
}