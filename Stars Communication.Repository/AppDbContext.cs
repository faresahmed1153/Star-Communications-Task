using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stars_Communication.Core.Models;
using Stars_Communication.Core.Models.Identity;
using System.Reflection;

namespace Stars_Communication.Repository
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}


		public DbSet<Tweet> Tweets { get; set; }

		public DbSet<UserFollow> UserFollows { get; set; }


	}
}
