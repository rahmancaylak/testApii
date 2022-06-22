using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using testApii.Entity;

namespace testApii.DAL
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<InjectionUnit> InjectionUnits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Santral> Santrals { get; set; }
    }
}