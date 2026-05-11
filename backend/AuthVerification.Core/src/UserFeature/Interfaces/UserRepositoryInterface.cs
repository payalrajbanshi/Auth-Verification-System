using AuthVerification.Core.src.UserFeature.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Interfaces
{
    public interface UserRepositoryInterface
    {
        public Task<UserEntity?> GetByIdAsync(long id);
        Task<UserEntity?> GetByUsernameAsync(string username);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity> AddAsync(UserEntity user);
        Task UpdateAsync(UserEntity user);
        Task SaveChangesAsync();
        Task UpdatePasswordAsync(long UserId, string passwordHash);


    }
}
