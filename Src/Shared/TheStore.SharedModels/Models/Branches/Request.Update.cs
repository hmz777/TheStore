using Microsoft.AspNetCore.Mvc;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "branches/{BranchId:int}";
		public override string Route => RouteTemplate.Replace("{BranchId:int}", BranchId.ToString());

		[FromRoute]
		public int BranchId { get; set; }

		[FromBody]
		public string Name { get; set; }

		[FromBody]
		public string Description { get; set; }

		[FromBody]
		public AddressDto Address { get; set; }

		[FromBody]
		public ImageDto Image { get; set; }
	}
}