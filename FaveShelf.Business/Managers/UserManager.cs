using FaveShelf.Business.Services;
using FaveShelf.Data.Entities;
using FaveShelf.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserManager(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterUser(UserEntity user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return false; // user zaten varsa 
            }

            user.Password = _passwordHasher.HashPassword(user, user.Password);

            await _userRepository.AddUser(user);
            return true; // yeni user eklendi    

        }

        public bool VerifyPassword(UserEntity user, string enteredPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
