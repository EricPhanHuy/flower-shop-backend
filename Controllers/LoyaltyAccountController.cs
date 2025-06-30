using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("/api/loyaltyaccount")]
    public class LoyaltyAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoyaltyAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/loyaltyaccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyAccount>>> GetAll()
        {
            return await _context.LoyaltyAccounts.ToListAsync();
        }

        // GET: api/loyaltyaccount/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<LoyaltyAccount>> Get(string userId)
        {
            var account = await _context.LoyaltyAccounts.FindAsync(userId);
            if (account == null)
            {
                return NotFound("Invalid UserId");
            }
            return account;
        }

        // POST: api/loyaltyaccount
        [HttpPost]
        public async Task<ActionResult<LoyaltyAccount>> Post([FromBody] LoyaltyAccount account)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == account.UserId);
            if (!userExists)
            {
                return BadRequest("User does not exist.");
            }

            // Kiểm tra đã có loyalty account chưa
            var existing = await _context.LoyaltyAccounts.FindAsync(account.UserId);
            if (existing != null)
            {
                return Conflict("Loyalty account already exists for this user.");
            }

            _context.LoyaltyAccounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { userId = account.UserId }, account);
        }

        // DELETE: api/loyaltyaccount/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var account = await _context.LoyaltyAccounts.FindAsync(userId);
            if (account == null)
            {
                return NotFound();
            }

            _context.LoyaltyAccounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}