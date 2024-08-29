using FaveShelf.Business.Dtos;
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

        public async Task<UserInfoDto> LoginUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null) { return null; } // user bulunamadı

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return new UserInfoDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                };
            }

            return null; // şifre yanlışsa 
        }

        public async Task<OperationResultDto> RegisterUser(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUser != null) 
            {
                return new OperationResultDto
                {
                    IsSucceed = false,
                    Message = "This email is taken"
                };
            }

            var user = new UserEntity
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Password = _passwordHasher.HashPassword(null, registerDto.Password)
            };

            await _userRepository.AddUser(user);

            return new OperationResultDto
            {
                IsSucceed = true,
                Message = "Successfull"
            };

        }

        public bool VerifyPassword(UserEntity user, string enteredPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<OperationResultDto> SaveFavoriteSong( int userId, FavoriteSongDto favoriteSongDto)
        {
            var user = await _userRepository.GetUserByEmail
        }
    }
}
