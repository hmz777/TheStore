﻿using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects.Keys
{
	public class EntityId<T> : ValueObject where T : notnull, IComparable
	{
		public T Id { get; private set; }

		public EntityId(T id)
		{
			Id = id;
		}

		public override string ToString()
		{
			return Id.ToString()!;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Id;
		}
	}
}