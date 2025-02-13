using Microsoft.EntityFrameworkCore;
using PrimeAspApi.Models;

namespace PrimeAspApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
    }
}
