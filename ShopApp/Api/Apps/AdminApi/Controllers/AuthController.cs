using Api.Apps.AdminApi.Dtos;
using Api.Services;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null || !user.IsAdmin)
                return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return BadRequest();

            var token = _jwtService.Generate(user, await _userManager.GetRolesAsync(user));

            return Ok(new { token = token });
        }

        //[HttpGet("createadmin")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        UserName = "Admin",
        //        FullName = "Maqsud Muslumov",
        //        Email = "muslumov98@mail.ru",
        //        IsAdmin = true,
        //    };
        //    await _userManager.CreateAsync(appUser, "Maqsud_1998");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Ok();
        //}
    }
}
