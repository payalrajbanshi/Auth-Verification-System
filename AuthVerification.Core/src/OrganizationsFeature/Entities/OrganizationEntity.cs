using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.Entities
{
    public class OrganizationEntity
    {
        public enum OrganizationStatus
        {
            InActive = 0,
            Active = 1
        }
        public long OrganizationId { get; private set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? TempEmail { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? Website { get; set; }
        public string? StreetAddress { get; set; }
        public string? EmailVerificationPin { get; private set; }
        //public string? MobileVerificationPin { get; private set; }
        public bool IsMobileNoVerified { get; set; } = false;
        public bool IsEmailVerified { get; set; }
        public OrganizationStatus Status { get; private set; } = OrganizationStatus.Active;
        public bool RowStatus { get; private set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;
        public TimeSpan CreatedTime { get; set; } = DateTime.UtcNow.TimeOfDay;
        public long CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        public void Activate()
        {
            Status = OrganizationStatus.Active;
        }
        public void Deactivate()
        {
            Status = OrganizationStatus.InActive;
        }
        public bool IsActive()
        {
            return Status == OrganizationStatus.Active;
        }
        public void SetEmailForVerification(string email, string pin)
        {
            TempEmail = email;
            EmailVerificationPin = pin;
            IsEmailVerified = false;
        }

        public bool ConfirmEmail(string pin)
        {
            if (EmailVerificationPin != pin)
                return false;

            Email = TempEmail;
            TempEmail = null;
            EmailVerificationPin = null;
            IsEmailVerified = true;
            return true;
        }
        //public void SetMobileForVerification(string mobile, string pin)
        //{
        //    MobileNo = mobile;
        //    MobileVerificationPin = pin;
        //    IsMobileNoVerified = false;
        //}

        //public bool ConfirmMobile(string pin)
        //{
        //    if (MobileVerificationPin != pin)
        //        return false;

        //    MobileVerificationPin = null;
        //    IsMobileNoVerified = true;
        //    return true;
        //}



    }
}
