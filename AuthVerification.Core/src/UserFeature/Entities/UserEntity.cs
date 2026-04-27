using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Entities
{
    public class UserEntity
    {
        public enum UserStatus
        {
            InActive = 0,
            Active = 1,
            Suspended = 2
        }

        public enum UserRole
        {
            SuperAdmin,
            Reseller,
            User,
            Organization
        }
        public long UserId { get; private set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? TempEmail { get; set; }
        public string? TempMobileNo { get; set; }
        public string PasswordHash { get; set; }
        public string? PasswordResetPin { get; set; }
        public DateTime? PasswordResetPinExpiresOn { get; set; }
        public UserRole UserType { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsMobileConfirmed { get; set; } = false;
        public string? EmailVerificationPin { get; set; }
        public string? MobileVerificationPin { get; set; }
        public UserStatus Status { get; private set; } = UserStatus.Active;
        public bool RowStatus { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long CreatedByUserId { get; set; }
        public UserEntity(long userId, UserStatus status) : this()
        {
            UserId = userId;
            Status = status;
        }
        public void Activate()
        {
            Status = UserStatus.Active;
        }
        public void Deactivate()
        {
            Status = UserStatus.InActive;
        }
        public bool IsActive()
        {
            return Status == UserStatus.Active;
        }
        public UserEntity()
        {
            Status = UserStatus.Active;
        }

        public void SetEmailForVerification(string email, string pin)
        {
            TempEmail = email;
            EmailVerificationPin = pin;
            IsEmailConfirmed = false;
        }
        public bool ConfirmEmail(string pin)
        {
            if (EmailVerificationPin != pin) return false;
            Email = TempEmail;
            TempEmail = null;
            EmailVerificationPin = null;
            IsEmailConfirmed = true;
            return true;
        }
        public void SetMobileForVerification(string mobile, string pin)
        {
            TempMobileNo = mobile;
            MobileVerificationPin = pin;
            IsMobileConfirmed = false;
        }
        public bool ConfirmPhone(string pin)
        {
            if (MobileVerificationPin != pin)
                return false;
            MobileNo = TempMobileNo;
            TempMobileNo = null;
            MobileVerificationPin = null;
            IsMobileConfirmed = true;
            return true;
        }


    }
}
