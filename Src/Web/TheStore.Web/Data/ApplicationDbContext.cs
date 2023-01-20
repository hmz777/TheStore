using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheStore.Web.Domain.Branches;
using TheStore.Web.Domain.Orders;
using TheStore.Web.Domain.Products;

namespace TheStore.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Order>()
				.HasMany(o => o.Products)
				.WithMany(p => p.Orders);

			builder.Entity<Order>()
				.HasOne(o => o.Branch)
				.WithMany(b => b.Orders);


			base.OnModelCreating(builder);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Branch> Branches { get; set; }
	}
}