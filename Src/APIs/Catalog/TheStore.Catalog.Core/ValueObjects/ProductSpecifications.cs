using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Text.Json;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class ProductSpecifications : ValueObject
	{
		private string JsonSpecs { get; }

		public ProductSpecifications(Dictionary<string, string> specs)
		{
			Guard.Against.Null(specs, nameof(specs));

			JsonSpecs = JsonSerializer.Serialize(specs);
		}

		public Dictionary<string, string> GetSpecifications()
			=> JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSpecs) ?? new Dictionary<string, string>();

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return JsonSpecs;
		}
	}
}
