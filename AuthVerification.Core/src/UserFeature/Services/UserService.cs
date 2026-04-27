using AuthVerification.Core.src.UserFeature.DTOs;
using AuthVerification.Core.src.UserFeature.Entities;
using AuthVerification.Core.src.UserFeature.Exceptions;
using AuthVerification.Core.src.UserFeature.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
namespace AuthVerification.Core.src.UserFeature.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepositoryInterface _userRepository;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;


        public UserService(
            UserRepositoryInterface userRepository,
            IEmailService emailService,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<long> CreateUserAsync(CreateUserDTO dto)
        {
            var emailExists = await _userRepository.GetByEmailAsync(dto.Email);
            if (emailExists != null)
                throw new ApplicationException("Email already exists");
            var pin = GeneratePin();

            var user = new UserEntity()
            {
                Name = dto.Name,
                Username = dto.Username,
                MobileNo = dto.MobileNo,
                TempEmail = dto.TempEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserType = Enum.Parse<UserEntity.UserRole>(dto.UserType)
            };

            user.SetEmailForVerification(dto.TempEmail, pin);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            await _emailService.SendAsync(
                dto.TempEmail,
                "Verify your email",
                $"Your verification PIN is {pin}"
            );

            return user.UserId;
        }

        public async Task UpdateUserAsync(UpdateUserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.userId);
            if (user == null)
                throw new UserNotFoundException(dto.userId);

            user.Name = dto.Name;
            user.Username = dto.Username;
            user.MobileNo = dto.MobileNo;
            user.Email = dto.Email;
            user.TempEmail = dto.TempEmail;
            user.UserType = Enum.Parse<UserEntity.UserRole>(dto.UserType);

            if (!string.IsNullOrEmpty(dto.TempEmail) &&
                dto.TempEmail != user.TempEmail)
            {
                var pin = GeneratePin();
                user.SetEmailForVerification(dto.TempEmail, pin);
            }

            await _userRepository.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.userId);
            if (user == null)
                throw new UserNotFoundException(dto.userId);

            var isValid = BCrypt.Net.BCrypt.Verify(dto.currentPassword, user.PasswordHash);
            if (!isValid)
                throw new UsernameOrPasswordDoesNotMatchException();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.newPassword);
            await _userRepository.UpdateAsync(user);

        }
        public async Task ConfirmEmailAsync(long userId, string pin)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            bool success = user.ConfirmEmail(pin);
            if (success)
            {
                await _userRepository.UpdateAsync(user);
            }


        }

        public async Task Activate(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            user.Activate();
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Deactivate(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            user.Deactivate();
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task AdminResetPasswordAsync(long userId, string newPassword)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdatePasswordAsync(userId, passwordHash);
        }

        public async Task GeneratePasswordResetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found");

            var pin = GeneratePin(6);
            user.PasswordResetPin = pin;
            user.PasswordResetPinExpiresOn = DateTime.UtcNow.AddMinutes(10);

            await _userRepository.UpdateAsync(user);

            var emailToSend = user.Email ?? user.TempEmail;
            if (!string.IsNullOrEmpty(emailToSend))
            {
                await _emailService.SendAsync(
                    emailToSend,
                    "Password Reset PIN",
                    $"Your password reset PIN is: {pin}"
                );
            }
        }

        public async Task ResetPasswordUsingPinAsync(ResetPasswordDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.email);

            if (user == null)
                throw new UserNotFoundException($"User with ID {dto.userId} does not exist.");
            var PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.newPassword);

            await _userRepository.UpdatePasswordAsync(user.UserId, PasswordHash);

        }

        public async Task SendMobileVerificationPinAsync(MobileVerificationDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.userId);
            if (user == null)
                throw new UserNotFoundException("User not found");

            var pin = GenerateNumericPin(6);
            user.SetMobileForVerification(user.TempMobileNo ?? user.MobileNo, pin);

            await _userRepository.UpdateAsync(user);
            await _smsService.SendAsync(user.TempMobileNo ?? user.MobileNo, $"Your mobile verification code is: {pin}");
        }

        public async Task ConfirmMobileAsync(long userId, string pin)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException("User not found");

            var success = user.ConfirmPhone(pin);
            if (!success)
                throw new ValidationException("Invalid mobile verification pin");

            await _userRepository.UpdateAsync(user);

        }

        private string GeneratePin(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = Random.Shared;

            for (int i = 0; i < length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            return new string(stringChars);
        }

        private string GenerateNumericPin(int length = 6)
        {
            var random = new Random();
            var pin = new StringBuilder();

            for (int i = 0; i < length; i++)
                pin.Append(random.Next(0, 10));

            return pin.ToString();
        }
    }
}
