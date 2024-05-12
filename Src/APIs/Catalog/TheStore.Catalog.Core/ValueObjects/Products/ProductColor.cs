using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductColor : ValueObject
	{
		public List<Image> Images { get; set; }
		public string ColorName { get; private set; }
		public string ColorCode { get; private set; }
		public bool IsMainColor { get; private set; }

		// Ef Core
		private ProductColor() { }

		public ProductColor(string colorName, string colorCode, bool isMainColor, List<Image> images = default!)
		{
			Guard.Against.NullOrEmpty(colorName, nameof(colorName));
			Guard.Against.NullOrEmpty(colorCode, nameof(colorCode));
			Guard.Against.InvalidFormat(colorCode, nameof(colorCode), regexPattern: "^(?:[0-9a-fA-F]{3,4}){1,2}$");

			ColorName = colorName;
			ColorCode = colorCode;
			IsMainColor = isMainColor;
			Images = images ?? [];
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ColorCode;
		}
	}
}