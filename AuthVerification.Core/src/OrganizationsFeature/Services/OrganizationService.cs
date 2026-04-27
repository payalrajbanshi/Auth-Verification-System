using AuthVerification.Core.src.OrganizationsFeature.DTOs;
using AuthVerification.Core.src.OrganizationsFeature.Entities;
using AuthVerification.Core.src.OrganizationsFeature.Interfaces;
using AuthVerification.Core.src.UserFeature.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.OrganizationsFeature.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepositoryInterface _organizationRepository;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public OrganizationService(
            IOrganizationRepositoryInterface organizationRepository,
            IEmailService emailService,
            ISmsService smsService)
        {
            _organizationRepository = organizationRepository;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<long> CreateOrganizationAsync(CreateOrganizationDTO dto, long createdByUserId)
        {
            var existing = await _organizationRepository.GetByCodeAsync(dto.Code);
            if (existing != null)
                throw new ApplicationException("Organization code already exists");
            var pin = GeneratePin();
            var organization = new OrganizationEntity
            {
                Code = dto.Code,
                Name = dto.Name,
                Email = null,
                TempEmail = dto.TempEmail,
                MobileNo = dto.MobileNo,
                PhoneNo = dto.PhoneNo,
                Website = dto.Website,
                StreetAddress = dto.StreetAddress,
                CreatedByUserId = createdByUserId,
                CreatedDate = DateTime.UtcNow.Date,
                CreatedTime = DateTime.UtcNow.TimeOfDay
            };
            organization.SetEmailForVerification(dto.TempEmail, pin);
            await _organizationRepository.AddAsync(organization);


            await _emailService.SendAsync(
                dto.TempEmail,
                "Verify organization email",
                $"Your verification PIN is {pin}"
            );

            return organization.OrganizationId;
        }

        public async Task UpdateOrganizationAsync(UpdateOrganizationDTO dto)
        {
            var organization = await _organizationRepository.GetByIdAsync(dto.OrganizationId);
            if (organization == null)
                throw new ValidationException("Organization not found");

            organization.Name = dto.Name;
            organization.Website = dto.Website;
            organization.PhoneNo = dto.PhoneNo;
            organization.StreetAddress = dto.StreetAddress;

            if (!string.IsNullOrWhiteSpace(dto.TempEmail) &&
                dto.TempEmail != organization.TempEmail)
            {
                var pin = GeneratePin();
                organization.SetEmailForVerification(dto.TempEmail, pin);

                await _emailService.SendAsync(
                    dto.TempEmail,
                    "Verify organization email",
                    $"Your verification PIN is {pin}"
                );
            }

            //if (!string.IsNullOrWhiteSpace(dto.MobileNo) &&
            //    dto.MobileNo != organization.MobileNo)
            //{
            //    var pin = GenerateNumericPin();
            //    organization.SetMobileForVerification(dto.MobileNo, pin);

            //    await _smsService.SendAsync(
            //        dto.MobileNo,
            //        $"Your mobile verification PIN is {pin}"
            //    );
            //}

            await _organizationRepository.UpdateAsync(organization);
        }

        public async Task Activate(long organizationId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId);
            if (organization == null)
                throw new ValidationException("Organization not found");

            organization.Activate();
            await _organizationRepository.UpdateAsync(organization);
        }

        public async Task Deactivate(long organizationId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId);
            if (organization == null)
                throw new ValidationException("Organization not found");

            organization.Deactivate();
            await _organizationRepository.UpdateAsync(organization);
        }

        public async Task VerifyEmailAsync(long organizationId, string pin)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId);
            if (organization == null)
                throw new ValidationException("Organization not found");

            var success = organization.ConfirmEmail(pin);
            if (!success)
                throw new ValidationException("Invalid email verification PIN");

            await _organizationRepository.UpdateAsync(organization);
        }

        //public async Task VerifyMobileAsync(long organizationId, string pin)
        //{
        //    var organization = await _organizationRepository.GetByIdAsync(organizationId);
        //    if (organization == null)
        //        throw new ValidationException("Organization not found");

        //    var success = organization.ConfirmMobile(pin);
        //    if (!success)
        //        throw new ValidationException("Invalid mobile verification PIN");

        //    await _organizationRepository.UpdateAsync(organization);
        //}


        private string GeneratePin(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var buffer = new char[length];
            var random = Random.Shared;

            for (int i = 0; i < length; i++)
                buffer[i] = chars[random.Next(chars.Length)];

            return new string(buffer);
        }

        //private string GenerateNumericPin(int length = 6)
        //{
        //    var random = new Random();
        //    var pin = new StringBuilder();

        //    for (int i = 0; i < length; i++)
        //        pin.Append(random.Next(0, 10));

        //    return pin.ToString();
        //}
    }
}
