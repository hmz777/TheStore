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
		[DisplayName("Birth Date")]
		public DateTimeOffset BirthDate { get; set; }

		[Required]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required]
		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }
    }
}