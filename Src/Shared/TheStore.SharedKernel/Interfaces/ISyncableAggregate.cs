namespace TheStore.SharedKernel.Interfaces
{
	public interface ISyncableAggregate
	{
		public bool NeedsSynchronization { get; set; }
	}
}