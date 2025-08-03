using FlowerShop_BackEnd.Database;
using FlowerShop_BackEnd.Models.DTOs;
using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/cart-items")]
    public class CartItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cart-items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetAll()
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Include(ci => ci.Cart)
                .ToListAsync();
        }

        // GET: api/cart-items/{cartId}/{productId}
        [HttpGet("{cartId}/{productId}")]
        public async Task<ActionResult<CartItem>> Get(int cartId, int productId)
        {
            var item = await _context.CartItems.FindAsync(cartId, productId);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/cart-items
        [HttpPost]
        public async Task<ActionResult<CartItem>> Create([FromBody] CartItemDTO dto)
        {
            var item = new CartItem
            {
                CartId = dto.CartId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { cartId = item.CartId, productId = item.ProductId }, item);
        }

        // PUT: api/cart-items/{cartId}/{productId}
        [HttpPut("{cartId}/{productId}")]
        public async Task<IActionResult> Update(int cartId, int productId, [FromBody] CartItemDTO dto)
        {
            if (cartId != dto.CartId || productId != dto.ProductId)
                return BadRequest("IDs do not match");

            var item = await _context.CartItems.FindAsync(cartId, productId);
            if (item == null)
                return NotFound();

            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/cart-items/{cartId}/{productId}
        [HttpDelete("{cartId}/{productId}")]
        public async Task<IActionResult> Delete(int cartId, int productId)
        {
            var item = await _context.CartItems.FindAsync(cartId, productId);
            if (item == null)
                return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

