namespace TheStore.SharedModels.Models
{
	public abstract class RequestBase : BaseMessage
	{
		internal abstract string Route { get; }
	}
}