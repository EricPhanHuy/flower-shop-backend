using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;


using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop_BackEnd.Controllers
{
    [Route("api/producttypes")]
    
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        [HttpPost("create")]
        public async Task<ActionResult<ProductType>> Create([FromBody] ProductType dto)
        {
            var productType = new ProductType
            {
                TypeName = dto.TypeName
            };

            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductTypes), new { id = productType.TypeId }, productType);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.ProductTypes.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ProductTypes.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
