﻿using System.ComponentModel;
using TheStore.Blazor.Models.ValueObjectsDtos;

namespace TheStore.Blazor.Models.Branches
{
	[DisplayName("Branch." + nameof(AddBranchImageRequest))]
	public class AddBranchImageRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}/image";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }

		public AddImageDto Image { get; set; }
	}
}