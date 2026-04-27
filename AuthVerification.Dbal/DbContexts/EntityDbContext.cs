using AuthVerification.Dbal.DbContexts.Mappings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AuthVerification.Core.src.UserFeature.Entities;
using AuthVerification.Dbal.DbContexts.Mappings;
using AuthVerification.Dbal.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using AuthVerification.Core.src.OrganizationsFeature.Entities;

namespace AuthVerification.Dbal.DbContexts
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<OrganizationEntity> Organizations => Set<OrganizationEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationEntityConfiguration());
        }

    }
}
