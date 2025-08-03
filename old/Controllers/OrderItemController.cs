using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerShop_BackEnd.Models;
using FlowerShop_BackEnd.Models.DTOs;
using FlowerShop_BackEnd.Database;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/orderitems")]
    public class OrderItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all order items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .ToListAsync();
        }

        // GET specific order item
        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderItem>> Get(int orderId, int productId)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (orderItem == null)
                return NotFound();

            return orderItem;
        }

        // POST: create new order item
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create([FromBody] OrderItemDTO dto)
        {
            var exists = await _context.OrderItems
                .AnyAsync(oi => oi.OrderId == dto.OrderId && oi.ProductId == dto.ProductId);

            if (exists)
                return Conflict("Order item already exists for this order and product.");

            var orderItem = new OrderItem
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPriceAtPurchase = dto.UnitPriceAtPurchase
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { orderId = dto.OrderId, productId = dto.ProductId }, orderItem);
        }

        // PUT: update quantity or unit price
        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> Update(int orderId, int productId, [FromBody] OrderItemDTO dto)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (orderItem == null)
                return NotFound();

            orderItem.Quantity = dto.Quantity;
            orderItem.UnitPriceAtPurchase = dto.UnitPriceAtPurchase;

            await _context.SaveChangesAsync();

            return Ok(orderItem);
        }

        // DELETE
        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (orderItem == null)
                return NotFound();

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
