using FaveShelf.Business.Services;
using FaveShelf.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using FaveShelf.Business.Dtos;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace FaveShelf.WebUI.Controllers
{
    [ApiController]
    [Route("api/account")]

    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISongService _songService;

        public AccountController(IUserService userService, ISongService songService)
        {
            _userService = userService;
            _songService = songService;
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
                    var claims = new List<Claim>
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim("email", user.Email),
                        new Claim("firstName", user.FirstName),
                        new Claim("lastName", user.LastName),
                        new Claim("userType", user.UserType.ToString()),


                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(48)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                    return Ok(new { Message = "Login successful.", User = user });
                }

                return Unauthorized("Invalid credentials.");
            }

            return BadRequest("Invalid data.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Logged out successfully.");
        }

        [Authorize]
        [HttpPost("save-favorite-song")] // Favori şarkıyı kaydetme için benzersiz bir rota
        public async Task<IActionResult> SaveFavoriteSong(FavoriteSongDto favoriteSongDto)
        {
            // Kullanıcının giriş yapıp yapmadığını kontrol edin
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }

            // Kullanıcının id claim'ini almayı deneyin
            var userIdClaim = User.FindFirst("id");

            // Eğer id claim'i bulunamazsa hata döndürün
            if (userIdClaim == null)
            {
                Console.WriteLine("User ID claim is null."); 
                return BadRequest("User ID not found in claims.");
            }

            // id claim'ini int'e çevirin
            var userId = int.Parse(userIdClaim.Value);

            Console.WriteLine(userId);
            //dtoya eklicem
            favoriteSongDto.UserId = userId;

            // Debug için log ekleyin
            Console.WriteLine($"UserID: {favoriteSongDto.UserId}, SongID: {favoriteSongDto.SongId}");

            var result = await _userService.SaveFavoriteSong(userId, favoriteSongDto);

            if (result.IsSucceed)
            {
                return Ok("Favorite song added.");
            }

            return BadRequest("Failed to add favorite song");
        }

        [HttpGet("get-favorite-song/{userId}")] // Favori şarkıyı alma için benzersiz bir rota ve GET methodu
        public async Task<IActionResult> GetFavoriteSong(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null || user.FavoriteSongId == null)
            {
                return NotFound("No favorite song found for the user.");
            }

            var favoriteSong = await _songService.GetSongById(user.FavoriteSongId.Value);
            return Ok(favoriteSong);
        }

    }
}
