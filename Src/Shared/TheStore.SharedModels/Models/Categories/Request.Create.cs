using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route => RouteTemplate;

		public int Id { get; set; }
		public int Order { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }

        public CreateRequest()
        {
            
        }

		public CreateRequest(int order, string name, bool active)
		{
			Order = order;
			Name = name;
			Active = active;
		}
	}
}