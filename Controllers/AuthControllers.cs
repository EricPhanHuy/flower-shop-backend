using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // ✅ POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDTO dto)
    {
        var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
            return Ok("User registered");

        return BadRequest(result.Errors);
    }

    // ✅ POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AccountDTO dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

        if (result.Succeeded)
            return Ok("Logged in");

        return Unauthorized("Invalid login attempt");
    }

    // ✅ POST: api/auth/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out");
    }
}
