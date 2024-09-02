using FaveShelf.Data.Context;
using FaveShelf.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FaveShelfContext _context;

        public UserRepository(FaveShelfContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();  
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateUser(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
