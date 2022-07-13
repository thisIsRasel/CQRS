using Domain.Aggregates.StudentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;

        public DbSet<Address> Addresses { get; set; } = default!;
    }
}
