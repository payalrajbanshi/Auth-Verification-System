using AuthVerification.Dbal.DbContexts.Mappings.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AuthVerification.Dbal.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AuthVerification.Dbal.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserDbModel> Users { get; set; } = null!;
        public DbSet<OrganizationDbModel> Organizations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserDbModelConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationDbModelConfiguration());
        }

    }
}
