using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using FlowerShop_BackEnd.Models.DTOs;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/occasions")]
    public class OccasionController : Controller
    {
        private readonly ApplicationDbContext _context;


        public OccasionController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Occasion>>> Index()
        {
            return await _context.Occasions.ToListAsync();
        }
    }
}
