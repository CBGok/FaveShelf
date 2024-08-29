using FaveShelf.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByEmail(string email);
        Task<UserEntity> GetUserById(int id);

        Task AddUser(UserEntity user);  
    }
}
