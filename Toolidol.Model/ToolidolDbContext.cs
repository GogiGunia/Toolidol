using Toolidol.Model.Model;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Toolidol.Model
{
    public class ToolidolDbContext : DbContext, IDataProtectionKeyContext
    {
        public ToolidolDbContext(DbContextOptions<ToolidolDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FacebookPage> FacebookPages { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToolidolDbContext).Assembly);
        }
    }
}
