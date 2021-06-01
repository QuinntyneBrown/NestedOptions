using NestedOptions.Api.Models;
using NestedOptions.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NestedOptions.Api.Data
{
    public class NestedOptionsDbContext: DbContext, INestedOptionsDbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Preferences> Preferences { get; private set; }
        public NestedOptionsDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NestedOptionsDbContext).Assembly);
        }
        
    }
}
