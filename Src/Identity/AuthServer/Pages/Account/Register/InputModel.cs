using AuthServer.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Pages.Account.Register
{
	public class InputModel
	{
		[Required]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[Required]
		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[Required]
		[DisplayName("Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DisplayName("Confirm Password")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),
			ErrorMessage = "Passwords_Not_Match")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DisplayName("Birth Date")]
		public DateTimeOffset BirthDate { get; set; }

		[Required]
		[DisplayName("Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }
}