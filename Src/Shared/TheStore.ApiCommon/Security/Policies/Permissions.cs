namespace TheStore.ApiCommon.Security.Policies
{
	public class Permissions
	{
		public const string Type = "Permission";
		public const string Prefix = "Permission:";
		public const string Read = Prefix + "Read";
		public const string WriteCreate = Prefix + "WriteCreate";
		public const string WriteUpdate = Prefix + "WriteUpdate";
		public const string WriteDelete = Prefix + "WriteDelete";
	}
}