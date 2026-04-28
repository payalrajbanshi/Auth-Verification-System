using AuthVerification.Dbal.DbContexts;
using AuthVerification.Dbal.DbModels;
using AuthVerification.Core.src.UserFeature.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static AuthVerification.Core.src.UserFeature.Entities.UserEntity;


namespace AuthVerification.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _db;
        public DatabaseSeeder(AppDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db)); ;
        }

        //public async Task SeedAsync()
        //{

        //    var superAdminExists = await _db.Users
        //    .AnyAsync(u => u.UserType ==UserRole.SuperAdmin && u.RowStatus == true);


        //    if (!superAdminExists)
        //    {
        //        var superAdmin = new UserDbModel
        //        {
        //            Username = "admin",
        //            Name = "admin",
        //            Email = "admin@example.com",
        //            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
        //            UserType = UserRole.SuperAdmin,
        //            IsEmailConfirmed = true,
        //            IsMobileConfirmed = true,
        //            Status = UserStatus.Active,
        //            RowStatus = true,
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedByUserId = 1
        //        };

        //        _db.Users.Add(superAdmin);
        //        await _db.SaveChangesAsync();
        //    }

        //}
        public async Task SeedAsync()
        {
            var admin = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == "admin");

            if (admin == null)
            {
                admin = new UserDbModel
                {
                    Username = "admin",
                    Name = "admin",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                    UserType = UserRole.SuperAdmin,
                    IsEmailConfirmed = true,
                    IsMobileConfirmed = true,
                    Status = UserStatus.Active,
                    RowStatus = true,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByUserId = 1
                };

                _db.Users.Add(admin);
            }
            else
            {
                // ensure correct role/state if it already exists
                admin.UserType = UserRole.SuperAdmin;
                admin.RowStatus = true;
                admin.Status = UserStatus.Active;
            }

            await _db.SaveChangesAsync();
        }

    }
}

