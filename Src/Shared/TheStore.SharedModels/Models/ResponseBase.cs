namespace TheStore.SharedModels.Models
{
	public class ResponseBase : BaseMessage
	{
		public ResponseBase(Guid correlationId) : base()
		{
			_correlationId = correlationId;
		}

		public ResponseBase() { }
	}
}
