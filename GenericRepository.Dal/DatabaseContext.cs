using GenericRepository.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Dal
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}