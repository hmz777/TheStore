using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductColor : ValueObject
	{
		public string ColorCode { get; }

		private List<Image> images;
		public IReadOnlyCollection<Image> Images => images.AsReadOnly();

		public ProductColor(string colorCode, List<Image> images)
		{
			Guard.Against.NullOrWhiteSpace(colorCode, nameof(colorCode));
			Guard.Against.InvalidFormat(colorCode, nameof(colorCode), "^#(?:[0-9a-fA-F]{3,4}){1,2}$");

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

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return ColorCode;

			foreach (var image in images)
			{
				yield return image;
			}
		}
	}
}