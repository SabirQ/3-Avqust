using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
        {

        }
        public DbSet<Car> Cars { get; set; }
    }
}
