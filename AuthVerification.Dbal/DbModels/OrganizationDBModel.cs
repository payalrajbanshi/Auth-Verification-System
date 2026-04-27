using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Dbal.DbModels
{
    public class OrganizationDbModel
    {
        public long OrganizationId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? TempEmail { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? Website { get; set; }
        public string? StreetAddress { get; set; }
        public bool IsMobileNoVerified { get; set; } = false;
        public bool IsEmailVerified { get; set; }
        public bool Status { get; set; }
        public bool RowStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan CreatedTime { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
