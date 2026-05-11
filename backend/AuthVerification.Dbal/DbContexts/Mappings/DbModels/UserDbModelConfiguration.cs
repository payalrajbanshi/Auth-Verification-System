using AuthVerification.Dbal.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthVerification.Dbal.DbContexts.Mappings.DbModels
{
    public class UserDbModelConfiguration : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .HasColumnName("user_id")
                .ValueGeneratedOnAdd();
            builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(u => u.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.MobileNo)
                .HasColumnName("mobile_no")
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(100);

            builder.Property(u => u.TempEmail)
                .HasColumnName("temp_email")
                .HasMaxLength(100);
            builder.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(u => u.PasswordResetPin)
                .HasColumnName("password_reset_pin")
                .HasMaxLength(20);

            builder.Property(u => u.PasswordResetPinExpiresOn)
                .HasColumnName("password_reset_pin_expires_on");
            builder.Property(u => u.UserType)
                .HasColumnName("user_type")
                .HasConversion<string>()
                //.HasColumnType("tinyint")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.LastLogin)
                .HasColumnName("last_login")
                .HasColumnType("datetime")
                .IsRequired(false);


            builder.Property(u => u.IsEmailConfirmed)
                   .HasColumnName("is_email_confirmed")
                          .HasColumnType("bit(1)")
       .HasConversion(v => v, v => v)
                   .IsRequired();

            builder.Property(u => u.IsMobileConfirmed)
                   .HasColumnName("is_mobile_confirmed")
                          .HasColumnType("bit(1)")
       .HasConversion(v => v, v => v)

                   .IsRequired();

            builder.Property(u => u.EmailVerificationPin)
                   .HasColumnName("email_verification_pin")
                   .HasMaxLength(20);


            builder.Property(u => u.MobileVerificationPin)
                   .HasColumnName("mobile_verification_pin")
                   .HasMaxLength(20);

            builder.Property(u => u.Status)
                   .HasColumnName("status")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(u => u.RowStatus)
                   .HasColumnName("row_status")
                          .HasColumnType("bit(1)")
       .HasConversion(v => v, v => v)
                   .IsRequired();

            builder.Property(u => u.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(u => u.CreatedByUserId)
                   .HasColumnName("created_by_user_id")
                   .HasColumnType("bigint")
                   .IsRequired();
            builder.Property(u => u.TempMobileNo)
                .HasColumnName("temp_mobile_no");

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
