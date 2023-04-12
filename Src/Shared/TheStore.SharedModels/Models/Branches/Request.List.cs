namespace TheStore.SharedModels.Models.Branches
{
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "branches";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}