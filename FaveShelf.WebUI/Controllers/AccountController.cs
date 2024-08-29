using FaveShelf.Business.Services;
using FaveShelf.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using FaveShelf.Business.Dtos;
using Microsoft.AspNetCore.Authentication;

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
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {

                var result = await _userService.RegisterUser(model);

                if (result.IsSucceed )
                {
                    return Ok(new { Message = result.Message});
                }
                else
                {
                    return BadRequest(new { Message = result.Message });
                }
            }

            return BadRequest("Invalid data.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.LoginUser(model);

                if (user != null)
                {
                    // Başarılı girişte, JWT token veya başka bir doğrulama yöntemi dönebiliriz
                    // Ancak şimdilik basit bir başarı mesajı dönüyoruz
                    return Ok(new { Message = "Login successful.", User = user });
                }

                return Unauthorized("Invalid credentials.");
            }

            return BadRequest("Invalid data.");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
