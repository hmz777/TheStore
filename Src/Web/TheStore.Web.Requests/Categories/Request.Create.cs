using System.ComponentModel;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Web.Requests.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;


		public CategoryDtoUpdate Category { get; set; }
	}
}