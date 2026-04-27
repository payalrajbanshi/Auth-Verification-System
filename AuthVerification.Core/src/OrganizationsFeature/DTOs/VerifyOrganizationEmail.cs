using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.DTOs
{
    public class VerifyOrganizationEmailDTO
    {
        [Required]
        public long OrganizationId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Pin { get; set; } = null!;
    }
}
