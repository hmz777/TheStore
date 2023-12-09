using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductColor : ValueObject
	{
		private readonly List<Image> images = new();

		public MultilanguageString ColorName { get; }
		public string ColorCode { get; }
		public bool IsMainColor { get; }

		[NotMapped]
		public ReadOnlyCollection<Image> Images => images.AsReadOnly();

		// Ef Core
		private ProductColor() { }

		public ProductColor(MultilanguageString colorName, string colorCode, bool isMainColor, List<Image> images)
		{
			Guard.Against.Null(colorName, nameof(colorName));
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

			return new ProductColor(ColorName, ColorCode, IsMainColor, newImages);
		}

		public ProductColor RemoveImage(Image image)
		{
			Guard.Against.Null(image, nameof(image));

			var newImages = new List<Image>(images);
			newImages.Remove(image);

			return new ProductColor(ColorName, ColorCode, IsMainColor, newImages);
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ColorCode;
		}
	}
}