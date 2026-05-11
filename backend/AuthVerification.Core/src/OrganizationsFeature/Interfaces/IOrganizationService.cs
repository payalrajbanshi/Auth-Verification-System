using AuthVerification.Core.src.OrganizationsFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.Interfaces
{
    public interface IOrganizationService
    {
        Task<long> CreateOrganizationAsync(CreateOrganizationDTO dto, long createdByUserId);
        Task UpdateOrganizationAsync(UpdateOrganizationDTO dto);

        Task Activate(long organizationId);
        Task Deactivate(long organizationId);

        Task VerifyEmailAsync(long organizationId, string pin);
        //Task VerifyMobileAsync(long organizationId, string pin);
    }
}
