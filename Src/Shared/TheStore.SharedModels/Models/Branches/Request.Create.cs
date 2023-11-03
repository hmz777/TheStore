using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;

		public MultilanguageStringDto Name { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public AddressDto Address { get; set; }
        public bool Published { get; set; }
    }
}