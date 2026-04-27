using AuthVerification.Dbal.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthVerification.Core.src.OrganizationsFeature.Entities;
using AuthVerification.Core.src.OrganizationsFeature.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace AuthVerification.Dbal.Repositories
{
    public class OrganizationRepository : IOrganizationRepositoryInterface
    {
        private readonly EntityDbContext _db;
        public OrganizationRepository(EntityDbContext db)
        {
            _db = db;
        }
        public async Task<OrganizationEntity?> GetByIdAsync(long organizationId)
        {
            return await _db.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == organizationId);
        }
        public async Task<OrganizationEntity?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;
            return await _db.Organizations.FirstOrDefaultAsync(o => o.Code == code);

        }

        //public async Task<List<OrganizationEntity>> GetAllAsync()
        //{
        //    return await _db.Organizations.Where(o => o.RowStatus == true)
        //        .ToListAsync();
        //}

        public async Task<OrganizationEntity> AddAsync(OrganizationEntity organization)
        {
            await _db.Organizations.AddAsync(organization);
            await _db.SaveChangesAsync();
            return organization;

        }
        public async Task UpdateAsync(OrganizationEntity organization)
        {
            _db.Organizations.Update(organization);
            await _db.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
