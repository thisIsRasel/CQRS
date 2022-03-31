using Domain.Aggregates.StudentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
