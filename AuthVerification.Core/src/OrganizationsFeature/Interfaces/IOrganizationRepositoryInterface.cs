using AuthVerification.Core.src.OrganizationsFeature.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.Interfaces
{
    public interface IOrganizationRepositoryInterface
    {
        Task<OrganizationEntity?> GetByIdAsync(long organizationId);
        Task<OrganizationEntity?> GetByCodeAsync(string code);
        // Task<List<OrganizationEntity>> GetAllAsync();
        //Task<OrganizationEntity?> GetByEmailAsync(string email);
        Task<OrganizationEntity> AddAsync(OrganizationEntity organization);
        Task UpdateAsync(OrganizationEntity organization);

        Task SaveChangesAsync();




    }
}
