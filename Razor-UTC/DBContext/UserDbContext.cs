using Microsoft.EntityFrameworkCore;
using Razor_UTC.Models;

namespace Razor_UTC.DBContext
{
    public class UserDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
    {
        public IConfiguration Configuration { get; } = configuration;
        public DbSet<UserInformation> UsersInformation { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = Configuration.GetConnectionString("connectionString")!;
            if (!string.IsNullOrEmpty(conn))
            optionsBuilder.UseSqlServer(conn);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInformation>().HasKey(x=>x.Id);
            modelBuilder.Entity<Registration>().HasKey(x=>x.Username);
        }
    }
}
