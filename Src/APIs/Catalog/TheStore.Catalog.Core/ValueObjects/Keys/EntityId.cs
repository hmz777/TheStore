using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects.Keys
{
    public class EntityId<T> : ValueObject
    {
        public T Id { get; private set; }

        public EntityId(T id)
        {
            Id = id;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
