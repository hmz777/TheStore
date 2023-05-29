namespace TheStore.Catalog.Core.ValueObjects.Keys
{
	public class ProductId : EntityId<int>
	{
		public ProductId(int id) : base(id)
		{
		}
	}
}