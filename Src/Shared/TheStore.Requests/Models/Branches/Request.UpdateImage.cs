using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.Requests;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Requests.Models.Branches
{
	[DisplayName("Branch." + nameof(UpdateImageRequest))]
	public class UpdateImageRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}/image";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		[FromRoute(Name = nameof(BranchId))]
		public int BranchId { get; set; }

		[FromForm]
		public UploadImageDto Image { get; set; }
	}
}