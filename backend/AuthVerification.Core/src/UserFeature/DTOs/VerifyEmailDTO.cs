using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class VerifyEmailDTO
    {
        public string TempEmail { get; set; }
        public string Pin { get; set; }
    }
}
