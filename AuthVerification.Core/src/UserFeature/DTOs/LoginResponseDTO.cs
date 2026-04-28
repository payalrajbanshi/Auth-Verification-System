using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class LoginResponseDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string? TempEmail { get; set; }
    }
}
