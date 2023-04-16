namespace TheStore.SharedModels.Models
{
	public abstract class RequestBase : BaseMessage
	{
		public abstract string Route { get; }
	}
}