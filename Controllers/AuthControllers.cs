using FlowerShop_BackEnd.Database;
using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

     //✅ POST: api/auth/register
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
    //[HttpPost("login")]
    //public async Task<IActionResult> Login([FromBody] AccountDTO dto)
    //{
    //    var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

    //    if (result.Succeeded)
    //        return Ok("Logged in");

    //    return Unauthorized("Invalid login attempt");
    //}
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AccountDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecureKeyOfAtLeast16Bytes"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "jwt-issuer",
            audience: "jwt-audience",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    // ✅ POST: api/auth/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out");
    }
}
