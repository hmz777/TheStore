﻿using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Web.Requests.Branches
{
	[DisplayName("Branch." + nameof(UpdateImageRequest))]
	public class UpdateImageRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}/image";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());


		public int BranchId { get; set; }


		public UploadImageDto Image { get; set; }
	}
}