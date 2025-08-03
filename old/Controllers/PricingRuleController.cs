using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/pricingrule")]
    public class PricingRuleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricingRuleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET "api/pricingrule/list"
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<PricingRule>>> GetPricingRules()
        {
            return await _context.PricingRules.ToListAsync();
        }

        // GET "api/pricingrule/${id}"
        [HttpGet("{id}")]
        public async Task<ActionResult<PricingRule>> GetPricingRule(int id)
        {
            var ResultRule = await _context.PricingRules.FindAsync(id);

            if (ResultRule == null)
            {
                return NotFound();
            }

            return ResultRule;
        }

        // POST "api/pricingrule/create
        [HttpPost("create")]
        public async Task<ActionResult<PricingRule>> CreatePricingRule([FromBody] PricingRule pricingRule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PricingRules.Add(pricingRule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPricingRule), new { id = pricingRule.RuleId }, pricingRule);
        }

        //PUT "api/pricing/rule/update/${id}"
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePricingRule(int id, PricingRule pricingRule)
        {
            if (id != pricingRule.RuleId)
            {
                return BadRequest();
            }

            _context.Entry(pricingRule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PricingRuleExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE "api/pricingrule/{id}"
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePricingRule(int id)
        {
            var pricingRule = await _context.PricingRules.FindAsync(id);

            if (pricingRule == null)
            {
                return NotFound();
            }

            _context.PricingRules.Remove(pricingRule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PricingRuleExist(int id)
        {
            return _context.PricingRules.Any(e => e.RuleId == id);
        }
    }
}