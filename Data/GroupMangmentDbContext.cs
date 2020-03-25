using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GroupMangmentDbContext : DbContext
    {
        public GroupMangmentDbContext(DbContextOptions<GroupMangmentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }

        public DbSet<GroupEntity> GroupEntities { get; set; }
    }
}
