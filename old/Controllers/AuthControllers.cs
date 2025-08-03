using FlowerShop_BackEnd.Database;
using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    // ✅ POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDTO dto)
    {
        var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);
        var cart = new Cart
        {
            UserId = user.Id
        };

        _context.Carts.Add(cart);
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
