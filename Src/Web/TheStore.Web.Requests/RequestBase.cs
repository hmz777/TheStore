namespace TheStore.Web.Requests
{
	public abstract class RequestBase : BaseMessage
	{
		public abstract string Route { get; }
	}
}