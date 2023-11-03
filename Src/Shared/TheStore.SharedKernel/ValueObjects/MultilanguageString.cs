﻿using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.SharedKernel.ValueObjects
{
	public class MultilanguageString : ValueObject
	{
		private readonly List<LocalizedString> localizedStrings = new();

		[NotMapped]
		public ReadOnlyCollection<LocalizedString> LocalizedStrings => localizedStrings.AsReadOnly();

		public MultilanguageString(List<LocalizedString> localizedStrings = null!)
		{
			this.localizedStrings = localizedStrings ?? new();
		}

		public string? GetString(CultureCode cultureCode)
			=> localizedStrings.Where(ls => ls.CultureCode == cultureCode).FirstOrDefault()?.Value;

		public void AddLocalizedString(LocalizedString localizedString)
		{
			Guard.Against.Null(localizedString, nameof(localizedString));

			if (localizedStrings.Contains(localizedString))
			{
				return;
			}

			localizedStrings.Add(localizedString);
		}

		public void RemoveLocalizedString(LocalizedString localizedString)
		{
			Guard.Against.Null(localizedString, nameof(localizedString));

			localizedStrings.Remove(localizedString);
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			foreach (var @string in localizedStrings)
			{
				yield return @string;
			}
		}
	}

	public class LocalizedString : ValueObject
	{
		public CultureCode CultureCode { get; }
		public string Value { get; }

		public LocalizedString(string value, CultureCode cultureCode)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));
			Guard.Against.Null(cultureCode, nameof(cultureCode));

			CultureCode = cultureCode;
			Value = value;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return CultureCode;
			yield return Value;
		}
	}

	public class CultureCode : ValueObject
	{
		public string Code { get; }

		public CultureCode(string code)
		{
			Guard.Against.NullOrEmpty(code, nameof(code));

			Code = code;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Code;
		}
	}
}