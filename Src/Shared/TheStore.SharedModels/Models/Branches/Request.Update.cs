using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		[FromRoute(Name = nameof(BranchId))]
		public int BranchId { get; set; }

		[FromBody]
		public BranchDtoUpdate Branch { get; set; }
	}
}