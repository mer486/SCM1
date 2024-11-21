using Microsoft.EntityFrameworkCore;

namespace SCM1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Store>? Stores { get; set; }
    }
}
