// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Pages.Login
{
	public class InputModel
	{
		[Required]
		[DisplayName("Email")]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[DisplayName("Remember Login")]
		public bool RememberLogin { get; set; }

		public string ReturnUrl { get; set; }

		public string Button { get; set; }
	}
}