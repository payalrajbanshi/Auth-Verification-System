using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class AdminResetPassword
    {
        public long UserId { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
