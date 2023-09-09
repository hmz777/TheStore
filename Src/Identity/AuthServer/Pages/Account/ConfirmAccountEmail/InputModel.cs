namespace AuthServer.Pages.Account.ConfirmAccountEmail
{
	public class InputModel
	{
		public string UserId { get; set; }
		public string Token { get; set; }
		public string ReturnUrl { get; set; }
	}
}
