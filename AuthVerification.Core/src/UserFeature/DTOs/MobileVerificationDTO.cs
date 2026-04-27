using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class MobileVerificationDTO
    {
        public long userId { get; set; }
        public string MobileNo { get; set; }
    }
}
