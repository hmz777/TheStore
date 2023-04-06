using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects.Keys
{
	public class EntityId<T> : ValueObject where T : IComparable
	{
		public T Id { get; private set; }

		public EntityId(T id)
		{
			Id = id;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Id;
		}
	}
}