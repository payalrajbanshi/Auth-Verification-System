using AuthVerification.Dbal.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthVerification.Core.src.UserFeature.Entities;
using AuthVerification.Core.src.UserFeature.Interfaces;

namespace AuthVerification.Dbal.Repositories
{
    public class UserRepository : UserRepositoryInterface
    {
        private readonly EntityDbContext _db;

        public UserRepository(EntityDbContext db)
        {
            _db = db;
        }
        public async Task<UserEntity?> GetByIdAsync(long userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }


        public async Task<UserEntity?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        //public async Task<UserEntity?> GetByEmailAsync(string email)
        //{
        //    return await _db.Users.FirstOrDefaultAsync(u => u.Email == email || u.TempEmail == email);
        //}
        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            return await _db.Users.FirstOrDefaultAsync(u =>
                (!string.IsNullOrEmpty(u.Email) && u.Email == email) ||
                (!string.IsNullOrEmpty(u.TempEmail) && u.TempEmail == email));
        }



        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task UpdateAsync(UserEntity user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task UpdatePasswordAsync(long userId, string password)
        {

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)

                throw new Exception("User not found");

            user.PasswordHash = password;
            //user.PasswordHash = HashPassword(password);
            await _db.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
