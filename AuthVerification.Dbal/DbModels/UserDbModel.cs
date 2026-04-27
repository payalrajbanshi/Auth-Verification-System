using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthVerification.Core.src.UserFeature.Entities;
using static AuthVerification.Core.src.UserFeature.Entities.UserEntity;
namespace AuthVerification.Dbal.DbModels
{
    public class UserDbModel
    {
        [Key]
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; } = null!;
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? TempEmail { get; set; }
        public string PasswordHash { get; set; }
        public string? PasswordResetPin { get; set; }
        public DateTime? PasswordResetPinExpiresOn { get; set; }
        //public string UserType { get; set; }
        public UserEntity.UserRole UserType { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsMobileConfirmed { get; set; }
        public string? EmailVerificationPin { get; set; }
        public string? MobileVerificationPin { get; set; }
        //public bool Status { get; set; } = true;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public bool RowStatus { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long CreatedByUserId { get; set; }
        public string? TempMobileNo { get; set; }
    }
}
