using System.ComponentModel;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Web.Requests.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;
		public BranchDtoUpdate Branch { get; set; }
	}
}