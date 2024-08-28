using FaveShelf.Business.Services;
using FaveShelf.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using FaveShelf.Data.Entities;

namespace FaveShelf.WebUI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password // Şifreyi hashlemek iyi bir uygulamadır
                };

                var result = await _userService.RegisterUser(user);

                if (result)
                {
                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("User with this email already exists.");
                }
            }

            return BadRequest("Invalid data.");
        }
    }
}
