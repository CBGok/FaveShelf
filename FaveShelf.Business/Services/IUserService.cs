using FaveShelf.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Business.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserEntity user);
    }
}
