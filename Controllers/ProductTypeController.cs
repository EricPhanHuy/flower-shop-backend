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
        public async Task<ActionResult<ProductType>> Create()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();


            using var jsonDoc = JsonDocument.Parse(body);
            var root = jsonDoc.RootElement;

            var productType = new ProductType
            {
                TypeName = root.GetProperty("typename").GetString()
            };
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        

    }
}
