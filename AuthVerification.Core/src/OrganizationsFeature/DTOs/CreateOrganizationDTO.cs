using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.DTOs
{
    public class CreateOrganizationDTO
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? TempEmail { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? Website { get; set; }
        public string? StreetAddress { get; set; }

    }
}
