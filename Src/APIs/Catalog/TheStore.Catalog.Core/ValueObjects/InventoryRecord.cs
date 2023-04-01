using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.Exceptions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class InventoryRecord : ValueObject
	{
		public int AvailableStock { get; private set; }
		public int RestockThreshold { get; private set; }
		public int MaxStockThreshold { get; private set; }
		public int OverStock { get; private set; }
		public bool OnReorder { get; private set; }

		[NotMapped]
		public bool HasOverStock => OverStock > 0;

		[NotMapped]
		public bool NeedsReorder => AvailableStock <= RestockThreshold;

		// Ef Core
        public InventoryRecord()
        {
            
        }

        public InventoryRecord(int availableStock, int restockThreshold, int maxStockThreshold, int overStock, bool onReorder)
		{
			Guard.Against.Negative(availableStock, nameof(availableStock));
			Guard.Against.NegativeOrZero(restockThreshold, nameof(restockThreshold));
			Guard.Against.NegativeOrZero(maxStockThreshold, nameof(maxStockThreshold));
			Guard.Against.Negative(overStock, nameof(overStock));

			if (availableStock > maxStockThreshold)
			{
				throw new StockExceededMaxThresholdException();
			}

			if (overStock > 0 && availableStock != maxStockThreshold)
			{
				throw new OverStockNotReachedException();
			}

			AvailableStock = availableStock;
			RestockThreshold = restockThreshold;
			MaxStockThreshold = maxStockThreshold;
			OverStock = overStock;
			OnReorder = onReorder;
		}

		public InventoryRecord AddStock(int quantity)
		{
			Guard.Against.NegativeOrZero(quantity, nameof(quantity));

			var stockState = AvailableStock + quantity;

			if (stockState > MaxStockThreshold)
			{
				var overStock = stockState - MaxStockThreshold;
				var availableStock = MaxStockThreshold;

				return new InventoryRecord(
					availableStock: availableStock,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: overStock,
					onReorder: false);
			}

			return new InventoryRecord(
					availableStock: stockState,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: 0,
					onReorder: OnReorder);
		}

		public InventoryRecord RemoveStock(int quantity)
		{
			Guard.Against.NegativeOrZero(quantity, nameof(quantity));

			if (AvailableStock == 0)
			{
				throw new ProductSoldOutException();
			}

			var completeStock = AvailableStock + OverStock;

			if (quantity > completeStock)
			{
				throw new NotEnoughItemsInInventoryException();
			}

			var remainingStock = completeStock - quantity;

			if (remainingStock > MaxStockThreshold)
			{
				var availableStock = MaxStockThreshold;
				var overStock = remainingStock - MaxStockThreshold;

				return new InventoryRecord(
					availableStock: availableStock,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: overStock,
					onReorder: OnReorder);

			}

			return new InventoryRecord(
					availableStock: remainingStock,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: 0,
					onReorder: OnReorder);
		}

		public InventoryRecord PutOnReorder()
		{
			return new InventoryRecord(
					availableStock: AvailableStock,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: OverStock,
					onReorder: true);
		}

		public InventoryRecord PutOffReorder()
		{
			return new InventoryRecord(
					availableStock: AvailableStock,
					restockThreshold: RestockThreshold,
					maxStockThreshold: MaxStockThreshold,
					overStock: OverStock,
					onReorder: false);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return AvailableStock;
			yield return RestockThreshold;
			yield return MaxStockThreshold;
			yield return OnReorder;
		}
	}
}
