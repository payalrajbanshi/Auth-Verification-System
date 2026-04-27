using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class ResetPasswordDTO
    {
        public long userId { get; set; }
        public string email { get; set; }
        public string pin { get; set; }
        public string newPassword { get; set; }
    }
}
