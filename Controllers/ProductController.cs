using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using FlowerShop_BackEnd.Models.DTOs;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: ProductController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Details(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;

        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> Create([FromBody] ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                TypeId = dto.TypeID,
                Condition = dto.Condition,
                Status = dto.Status,
                BasePrice = dto.BasePrice,
                StockQuantity = dto.StockQuantity
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return (product);
        }

    }
}
