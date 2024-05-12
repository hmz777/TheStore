namespace TheStore.Web.Requests
{
	public class BaseMessage
	{
		/// <summary>
		/// Unique Identifier used by logging
		/// </summary>
		protected Guid _correlationId = Guid.NewGuid();
		public Guid CorrelationId => _correlationId;
	}
}