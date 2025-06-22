using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerShop_BackEnd.Models;
using FlowerShop_BackEnd.Models.DTOs;
using FlowerShop_BackEnd.Database;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            return await _context.Payments.Include(p => p.Order).ToListAsync();
        }

        // GET single payment by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(int id)
        {
            var payment = await _context.Payments.Include(p => p.Order).FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound();

            return payment;
        }

        // POST: create a payment
        [HttpPost]
        public async Task<ActionResult<Payment>> Create([FromBody] PaymentDTO dto)
        {
            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                PaymentDate = dto.PaymentDate,
                TransactionReference = dto.TransactionReference
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = payment.PaymentId }, payment);
        }

        // PUT: update a payment
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentDTO dto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            payment.OrderId = dto.OrderId;
            payment.PaymentMethod = dto.PaymentMethod;
            payment.PaymentStatus = dto.PaymentStatus;
            payment.PaymentDate = dto.PaymentDate;
            payment.TransactionReference = dto.TransactionReference;

            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        // DELETE: remove a payment
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
