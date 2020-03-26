using Data.Configurations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GroupMangmentDbContext : DbContext
    {
        public GroupMangmentDbContext(DbContextOptions<GroupMangmentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        }

        public DbSet<GroupEntity> GroupEntities { get; set; }
    }
}
