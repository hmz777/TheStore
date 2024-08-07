using AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data.Extensions
{
    public static class Data
    {
        public static async Task MigrateAndSeed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var hasUsers = await userManager.Users.AnyAsync();

                if (hasUsers == false)
                {
                    await userManager.CreateAsync(new ApplicationUser
                    {
                        FirstName = "Test 1 First",
                        LastName = "Test 1 Last",
                        Email = "test1@test.com",
                        UserName = "testUser1",
                        PhoneNumber = "+4911111111111",
                        PhoneNumberConfirmed = false,
                        EmailConfirmed = true,
                        Active = true,
                    }, "Password@123");

                    await userManager.CreateAsync(new ApplicationUser
                    {
                        FirstName = "Test 2 First",
                        LastName = "Test 2 Last",
                        Email = "test2@test.com",
                        UserName = "testUser2",
                        PhoneNumber = "+4911111111112",
                        PhoneNumberConfirmed = false,
                        EmailConfirmed = true,
                        Active = true,
                    }, "Password@123");

                    await userManager.CreateAsync(new ApplicationUser
                    {
                        FirstName = "Test 3 First",
                        LastName = "Test 3 Last",
                        Email = "test3@test.com",
                        UserName = "testUser3",
                        PhoneNumber = "+4911111111113",
                        PhoneNumberConfirmed = false,
                        EmailConfirmed = true,
                        Active = true,
                    }, "Password@123");
                }
            }
        }
    }
}