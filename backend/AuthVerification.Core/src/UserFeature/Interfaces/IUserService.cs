using AuthVerification.Core.src.UserFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Interfaces
{
    public interface IUserService
    {
        Task<long> CreateUserAsync(CreateUserDTO dto);
        Task UpdateUserAsync(UpdateUserDTO dto);
        Task ConfirmEmailAsync(long userId, string pin);

        Task Activate(long userId);
        Task Deactivate(long userId);
        Task ChangePasswordAsync(ChangePasswordDTO dto);
        Task AdminResetPasswordAsync(long userId, string newPassword);
        Task ConfirmMobileAsync(long UserId, string Pin);
        Task GeneratePasswordResetByEmailAsync(string email);
        Task ResetPasswordUsingPinAsync(ResetPasswordDTO dto);

    }
}
