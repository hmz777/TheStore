using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Pages.Account.Profile.Password
{
	public class UpdatePasswordViewModel
	{
		[DisplayName("Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[DisplayName("Confirm Password")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}