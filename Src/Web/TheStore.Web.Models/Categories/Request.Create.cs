using System.ComponentModel;

namespace TheStore.Web.Models.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;


		public CategoryDtoUpdate Category { get; set; }
	}
}