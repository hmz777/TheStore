namespace TheStore.SharedModels.Models.Branches
{
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;

		public string Name { get; set; }
	}
}