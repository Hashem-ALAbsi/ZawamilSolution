using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zawamil.Core.Models.Songes;
using Zawamil.Core.Models.Users;

namespace Zawamil.EF
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<User>? Users { get; set; }
		public DbSet<Song>? Songs { get; set; }
		public DbSet<AppVersion>? AppVersions { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	if (!optionsBuilder.IsConfigured)
		//	{
		//		optionsBuilder.UseSqlServer("YourConnectionString");
		//	}
		//}
	}

	//public void ConfigureServices(IServiceCollection services)
	//{
	//	services.AddDbContext<ApplicationDbContext>(options =>
	//		options.UseSqlServer(
	//			"DefaultConnection",
	//			b => b.MigrationsAssembly("Zawamil.Api")));
	//}
}