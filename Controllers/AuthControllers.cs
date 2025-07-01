using FlowerShop_BackEnd.Models;
using FlowerShop_BackEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _jwtService;
    public AuthController(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    // ✅ POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.Phone
        };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
            return Ok("User registered");

        return BadRequest(result.Errors);
    }

    // ✅ POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(dto.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid user");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName!, dto.Password, false, false);
        if (result.Succeeded)
        {
            var jwtToken = _jwtService.GenerateToken(user);
            return Ok(new
            {
                message = "Login successfull!",
                token = jwtToken,
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    name = user.FullName
                }
            });
        }

        return Unauthorized("Wrong password");
    }

    // ✅ POST: api/auth/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out");
    }
}
