namespace TheStore.Web.Models
{
	public abstract class RequestBase : BaseMessage
	{
		public abstract string Route { get; }
	}
}