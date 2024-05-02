using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TheStore.SharedKernel.ValueObjects
{
	public class MultilanguageString : ValueObject
	{
		[NotMapped]
		private bool dataDeserialized = false;

		[NotMapped]
		private List<LocalizedString> localizedStrings = new();

		[NotMapped]
		public ReadOnlyCollection<LocalizedString> LocalizedStrings => localizedStrings.AsReadOnly();

		private string? jsonBack;

		public string? Json
		{
			get { return jsonBack; }
			set
			{
				jsonBack = value;
				LoadJsonIfNotLoaded();
			}
		}

		// Ef Core
		private MultilanguageString() { }

		public MultilanguageString(List<LocalizedString>? localizedStrings = null!)
		{
			if (localizedStrings != null)
			{
				this.localizedStrings = localizedStrings;
				SaveJson();
			}
		}

		public MultilanguageString(LocalizedString initialLocalizedString)
		{
			Guard.Against.Null(initialLocalizedString, nameof(initialLocalizedString));

			localizedStrings.Add(initialLocalizedString);

			SaveJson();
		}

		public MultilanguageString(string value, CultureCode cultureCode)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));
			Guard.Against.Null(cultureCode, nameof(cultureCode));

			var initialLocalizedString = new LocalizedString(value, cultureCode);
			localizedStrings.Add(initialLocalizedString);

			SaveJson();
		}

		private void LoadJsonIfNotLoaded()
		{
			if (dataDeserialized == false && string.IsNullOrEmpty(Json) == false)
			{
				localizedStrings = JsonSerializer.Deserialize<List<LocalizedString>>(Json)!;

				if (localizedStrings == null)
				{
					throw new Exception("Error deserializing json");
				}

				dataDeserialized = true;
			}
		}

		private void SaveJson()
		{
			Json = JsonSerializer.Serialize(localizedStrings);
		}

		public string? GetString(CultureCode cultureCode)
		{
			return localizedStrings.Find(ls => ls.CultureCode == cultureCode)?.Value;
		}

		public void AddLocalizedString(LocalizedString localizedString)
		{
			Guard.Against.Null(localizedString, nameof(localizedString));

			if (localizedStrings.Contains(localizedString))
			{
				return;
			}

			localizedStrings.Add(localizedString);

			SaveJson();
		}

		public void AddLocalizedString(string value, CultureCode cultureCode)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));
			Guard.Against.Null(cultureCode, nameof(cultureCode));

			var localizedString = new LocalizedString(value, cultureCode);

			if (localizedStrings.Contains(localizedString))
			{
				return;
			}

			localizedStrings.Add(localizedString);

			SaveJson();
		}

		public void RemoveLocalizedString(LocalizedString localizedString)
		{
			Guard.Against.Null(localizedString, nameof(localizedString));

			localizedStrings.Remove(localizedString);

			SaveJson();
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Json!;
		}
	}

	[NotMapped]
	public class LocalizedString : ValueObject
	{
		public CultureCode CultureCode { get; }
		public string Value { get; }

		// Ef Core
		private LocalizedString() { }

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

	[NotMapped]
	public class CultureCode : ValueObject
	{
		public static readonly CultureCode English = new("en-US");
		public static readonly CultureCode Arabic = new("ar-SY");

		public string Code { get; }

		// Ef Core
		private CultureCode() { }

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