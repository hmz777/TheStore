﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.SharedKernel.Entities
{
	public abstract class BaseEntity<TId>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public TId Id { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public DateTimeOffset DateUpdated { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj is not BaseEntity<TId> other)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (GetType() != other.GetType())
				return false;

			if (EqualityComparer<TId>.Default.Equals(Id, default) ||
				EqualityComparer<TId>.Default.Equals(other.Id, default))
				return false;

			return EqualityComparer<TId>.Default.Equals(Id, other.Id);
		}

		public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return (GetType().ToString() + Id).GetHashCode();
		}
	}
}