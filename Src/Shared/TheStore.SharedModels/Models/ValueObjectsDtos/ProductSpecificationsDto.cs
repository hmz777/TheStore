using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ProductSpecificationsDto))]
	public class ProductSpecificationsDto
	{
        public Dictionary<string,string> Specs { get; set; }
    }
}