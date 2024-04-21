using AuthServer.Models;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthServer.Services.Profile
{
	public class ProfileService : IProfileService
	{
		private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
		private readonly UserManager<ApplicationUser> userManager;

		public ProfileService(
			IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
			UserManager<ApplicationUser> userManager)
		{
			this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
			this.userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var id = context.Subject.GetSubjectId();
			var user = await userManager.FindByIdAsync(id) ?? throw new Exception("User not found!");

			var claimsPrincipal = await userClaimsPrincipalFactory.CreateAsync(user);
			var claims = claimsPrincipal.Claims.ToList();

			claims.Add(new Claim("first_name", user.FirstName));
			claims.Add(new Claim("last_name", user.LastName));
			claims.Add(new Claim("active", user.Active.ToString()));
			claims.Add(new Claim("birthdate", user.BirthDate.ToString()));

			context.IssuedClaims.AddRange(claims);
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var id = context.Subject.GetSubjectId();
			var user = await userManager.FindByIdAsync(id);
			context.IsActive = user != null && user.Active;
		}
	}
}