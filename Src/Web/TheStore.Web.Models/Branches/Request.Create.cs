using System.ComponentModel;

namespace TheStore.Web.Models.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;


		public BranchDtoUpdate Branch { get; set; }
	}
}