namespace AuthServer.Services.Emails
{
	public class EmailOptions
	{
		public const string Key = nameof(EmailOptions);

		public string Server { get; set; }
		public int Port { get; set; }
		public string SecurityEmail { get; set; }
		public string InfoEmail { get; set; }
	}
}