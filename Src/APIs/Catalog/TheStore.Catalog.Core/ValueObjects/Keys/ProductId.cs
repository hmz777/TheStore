using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.ValueObjects.Keys
{
	public class ProductId : EntityId<int>
	{
		public ProductId(int id) : base(id) { }

		public override bool Equals(object obj)
		{
			if (obj is null || obj is not ProductId productId)
			{
				return false;
			}

			return this.Id == productId.Id;
		}

		public override int GetHashCode() => this.Id.GetHashCode();
	}
}