using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

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
		public MultilanguageStringDto Name { get; set; }

		[FromBody]
		public MultilanguageStringDto Description { get; set; }

		[FromBody]
		public AddressDto Address { get; set; }

		[FromBody]
		public bool Published { get; set; }
	}
}