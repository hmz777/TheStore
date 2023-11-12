using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;

		[FromBody]
		public BranchDtoUpdate Branch { get; set; }
	}
}