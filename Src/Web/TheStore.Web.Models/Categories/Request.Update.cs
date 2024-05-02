using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TheStore.Web.Models.Categories
{
	[DisplayName("Category." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		
		public int CategoryId { get; set; }

		
		public CategoryDtoUpdate Category { get; set; }
	}
}