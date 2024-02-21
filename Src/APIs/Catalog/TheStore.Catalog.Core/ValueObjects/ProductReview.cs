using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class ProductReview : ValueObject
	{
		public string Title { get; }
		public DateTimeOffset Date { get; }
		public string Content { get; }
		public int Rating { get; }
		public string User { get; }

		// Ef Core
		private ProductReview() { }

		public ProductReview(string title, DateTimeOffset date, string content, int rating, string user)
		{
			Guard.Against.NullOrEmpty(title, nameof(title));
			Guard.Against.NullOrEmpty(content, nameof(content));
			Guard.Against.OutOfRange(rating, nameof(rating), 1, 5);
			Guard.Against.NullOrEmpty(user, nameof(user));

			Title = title;
			Date = date;
			Content = content;
			Rating = rating;
			User = user;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Title;
			yield return Content;
			yield return Rating;
			yield return User;
			yield return Date;
		}
	}
}