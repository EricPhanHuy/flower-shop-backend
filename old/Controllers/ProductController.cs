using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using FlowerShop_BackEnd.Models.DTOs;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Index([FromQuery] ProductFilterDTO filter)
        {
            var query = _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.Occasions)
                .AsQueryable();

            // Filter by name (case-insensitive partial match)
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            // Filter by ProductType
            if (filter.ProductTypeId.HasValue)
            {
                query = query.Where(p => p.TypeId == filter.ProductTypeId.Value);
            }

            // Filter by Occasion
            if (filter.OccasionId.HasValue)
            {
                query = query.Where(p => p.Occasions.Any(o => o.Id == filter.OccasionId.Value));
            }

            // Filter by price range
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            // Apply pagination
            var page = filter.Page ?? 1;
            var pageSize = filter.PageSize ?? 10;
            var skip = (page - 1) * pageSize;

            var products = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return Ok(products);
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
                Status = dto.Status,
                Price = dto.BasePrice,
                StockQuantity = dto.StockQuantity
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return (product);
        }

    }
}
