using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductColor : ValueObject
	{
		private readonly List<Image> images = new();

		public string ColorName { get; }
		public string ColorCode { get; }
		public bool IsMainColor { get; }

		[NotMapped]
		public ReadOnlyCollection<Image> Images => images.AsReadOnly();

		// Ef Core
		private ProductColor() { }

		public ProductColor(string colorName, string colorCode, bool isMainColor, List<Image> images)
		{
			Guard.Against.NullOrEmpty(colorName, nameof(colorName));
			Guard.Against.NullOrEmpty(colorCode, nameof(colorCode));
			Guard.Against.InvalidFormat(colorCode, nameof(colorCode), regexPattern: "^(?:[0-9a-fA-F]{3,4}){1,2}$");

			ColorName = colorName;
			ColorCode = colorCode;
			IsMainColor = isMainColor;
			this.images = images ?? new List<Image>();
		}

		public ProductColor AddImage(Image image)
		{
			Guard.Against.Null(image, nameof(image));

			var newImages = new List<Image>(images)
			{
				image
			};

			return new ProductColor(ColorCode, IsMainColor, newImages);
		}

		public ProductColor RemoveImage(Image image)
		{
			Guard.Against.Null(image, nameof(image));

			var newImages = new List<Image>(images);
			newImages.Remove(image);

			return new ProductColor(ColorCode, IsMainColor, newImages);
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ColorCode;
		}
	}
}