// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServer.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public bool Active { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTimeOffset BirthDate { get; set; }


		[NotMapped]
		public string FullName => $"{FirstName} {LastName}";
	}
}