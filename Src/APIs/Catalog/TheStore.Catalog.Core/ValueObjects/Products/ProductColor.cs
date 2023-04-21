using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductColor : ValueObject
	{
		public string ColorCode { get; private set; }

		private List<Image> images = new();

		[NotMapped]
		public ReadOnlyCollection<Image> Images => images.AsReadOnly();

		// Ef Core
		private ProductColor()
		{

		}

		public ProductColor(string colorCode, List<Image> images)
		{
			Guard.Against.NullOrWhiteSpace(colorCode, nameof(colorCode));
			Guard.Against.InvalidFormat(colorCode, nameof(colorCode), regexPattern: "^#(?:[0-9a-fA-F]{3,4}){1,2}$");

			ColorCode = colorCode;
			this.images = images ?? new List<Image>();
		}

		public ProductColor AddImage(Image image)
		{
			Guard.Against.Null(image, nameof(image));

			var newImages = new List<Image>(images)
			{
				image
			};

			return new ProductColor(ColorCode, newImages);
		}

		public ProductColor RemoveImage(Image image)
		{
			Guard.Against.Null(image, nameof(image));

			var newImages = new List<Image>(images);
			newImages.Remove(image);

			return new ProductColor(ColorCode, newImages);
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ColorCode;

			foreach (var image in images)
			{
				yield return image;
			}
		}
	}
}