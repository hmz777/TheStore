﻿using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Branches
{
	[DisplayName("Branch." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "Branches.Id";
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		public int BranchId { get; set; }
	}
}