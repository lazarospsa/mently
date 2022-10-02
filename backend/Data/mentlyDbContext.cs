using Microsoft.EntityFrameworkCore;
using mently.Models;

namespace mently.Data
{
    public class mentlyDbContext : DbContext
    {
        public mentlyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}