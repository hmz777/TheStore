using FluentAssertions;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class InventoryRecordSpec
	{
		[Theory]
		[InlineData(-1, 0, 0, 0, false)]
		[InlineData(0, -1, 0, 0, false)]
		[InlineData(0, 1, -1, 0, false)]
		[InlineData(1, 1, 1, -1, false)]
		[InlineData(1, 0, 0, 0, false)]
		[InlineData(1, 1, 0, 0, false)]
		[InlineData(50, 10, 5, 0, false)] // Available stock is bigger thatn max threshold	
		[InlineData(50, 10, 100, 10, false)] // Stock and overstock arent bigger thatn max thresold (invalid overstock)	
		public void Shouldnt_Create_Inventory_Record_With_Invalid_Data(
			int availableStock, int restockThreshold, int maxStockThreshold, int overStock, bool onReorder)
		{
			var action = () => new InventoryRecord(availableStock,
												   restockThreshold,
												   maxStockThreshold,
												   overStock,
												   onReorder);


			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Add_Stock()
		{
			var iRecord = new InventoryRecord(50, 10, 100, 0, false).AddStock(15);

			iRecord.AvailableStock.Should().Be(65);
		}

		[Fact]
		public void Adding_Stock_More_Than_MaxThresold_Will_Create_Overstock()
		{
			var iRecord = new InventoryRecord(50, 10, 100, 0, false).AddStock(60);

			iRecord.OverStock.Should().Be(10);
		}

		[Fact]
		public void Can_Remove_Stock()
		{
			var iRecord = new InventoryRecord(50, 10, 100, 0, false).RemoveStock(15); ;

			iRecord.AvailableStock.Should().Be(35);
		}

		[Fact]
		public void Cant_Remove_More_Than_Available_Stock()
		{
			var action = () => new InventoryRecord(50, 10, 100, 0, false).RemoveStock(60);

			action.Should().Throw<NotEnoughItemsInInventoryException>();
		}

		[Fact]
		public void Cant_Remove_If_Stock_Is_Empty()
		{
			var action = () => new InventoryRecord(0, 10, 100, 0, false).RemoveStock(10);

			action.Should().Throw<ProductSoldOutException>();
		}

		[Fact]
		public void Removing_Stock_Will_Remove_From_Overstock_First()
		{
			var iRecord = new InventoryRecord(100, 10, 100, 40, false).RemoveStock(10);

			iRecord.OverStock.Should().Be(30);
		}

		[Fact]
		public void Removing_Stock_Will_Remove_From_Overstock_First_Then_Available_Stock()
		{
			var iRecord = new InventoryRecord(100, 10, 100, 40, false).RemoveStock(50);

			iRecord.AvailableStock.Should().Be(90);
		}

		[Fact]
		public void Can_Put_Record_On_Reorder()
		{
			var iRecord = new InventoryRecord(100, 10, 100, 40, false).PutOnReorder();

			iRecord.OnReorder.Should().Be(true);
		}

		[Fact]
		public void Can_Put_Record_Off_Reorder()
		{
			var iRecord = new InventoryRecord(100, 10, 100, 40, false).PutOffReorder();

			iRecord.OnReorder.Should().Be(false);
		}
	}
}