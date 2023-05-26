using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(UpdateImageRequest))]
	public class UpdateImageRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}/image";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		[FromRoute(Name = nameof(BranchId))]
		public int BranchId { get; set; }

		[FromForm]
		public UpdateImageDto Image { get; set; }
	}
}