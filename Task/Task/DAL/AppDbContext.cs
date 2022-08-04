using Microsoft.EntityFrameworkCore;
using Task.DAL.Configurations;
using Task.Models;

namespace Task.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Engine> Engines { get; set; }
    }
}
