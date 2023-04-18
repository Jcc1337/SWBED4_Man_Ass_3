using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Guest> Guests => Set<Guest>();
		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//	base.OnModelCreating(builder);
  //          ModelBuilder.Entity<Guest>().HasData(new Guest { });
		//}
	}
}