﻿using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class ProductReview : IAggregateRoot
	{
		public ProductId ProductId { get; set; }
		public string Title { get; set; }
		public DateTimeOffset Date { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
		public string User { get; set; }
		public bool Published { get; set; }

		// Ef Core
		private ProductReview() { }

		public ProductReview(ProductId productId, string title, DateTimeOffset date, string content, int rating, string user, bool published)
		{
			Guard.Against.NegativeOrZero(productId.Id, nameof(productId));
			Guard.Against.NullOrEmpty(title, nameof(title));
			Guard.Against.NullOrEmpty(content, nameof(content));
			Guard.Against.OutOfRange(rating, nameof(rating), 1, 5);
			Guard.Against.NullOrEmpty(user, nameof(user));

			ProductId = productId;
			Title = title;
			Date = date;
			Content = content;
			Rating = rating;
			User = user;
			Published = published;
		}
	}
}