namespace TheStore.SharedModels.Models.Category
{
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }
		public int Order { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }
	}
}
