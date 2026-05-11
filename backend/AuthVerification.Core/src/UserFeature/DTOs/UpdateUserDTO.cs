using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.DTOs
{
    public class UpdateUserDTO
    {
        public long userId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? TempEmail { get; set; }
        public string? Password { get; set; }
        public string UserType { get; set; }
    }
}
