using AuthVerification.Dbal.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Dbal.DbContexts.Mappings.DbModels
{
    public class OrganizationDbModelConfiguration : IEntityTypeConfiguration<OrganizationDbModel>

    {
        public void Configure(EntityTypeBuilder<OrganizationDbModel> builder)
        {
            builder.ToTable("organizations");

            builder.HasKey(o => o.OrganizationId);

            builder.Property(o => o.OrganizationId)
                .HasColumnName("organization_id")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Code)
                .HasColumnName("code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Name)
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.Email)
                .HasColumnName("email")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(o => o.TempEmail)
                .HasColumnName("temp_email")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(o => o.MobileNo)
                .HasColumnName("mobile_no")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(o => o.PhoneNo)
                .HasColumnName("phone_no")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(o => o.Website)
                .HasColumnName("website")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(o => o.StreetAddress)
                .HasColumnName("street_address")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(o => o.IsEmailVerified)
                .HasColumnName("is_email_verified")
                .IsRequired();

            builder.Property(o => o.IsMobileNoVerified)
                .HasColumnName("is_mobile_no_verified")
                .IsRequired();

            builder.Property(o => o.Status)
                .HasColumnName("status")
                .HasColumnType("tinyint(1)")
                .HasConversion<bool>()
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(o => o.RowStatus)
                .HasColumnName("row_status")
                .HasColumnType("tinyint(1)")
                .HasConversion<bool>()
                .HasDefaultValue(true)
                .IsRequired();


            builder.Property(o => o.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(o => o.CreatedTime)
                .HasColumnName("created_time")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(o => o.CreatedByUserId)
                .HasColumnName("created_by_user_id")
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.HasIndex(o => o.Code)
                .IsUnique();
        }
    }
}
