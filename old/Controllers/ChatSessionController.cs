using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/chatsession")]
    public class ChatSessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatSessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/chatsession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatSession>>> GetChatSessions()
        {
            return await _context.ChatSessions.ToListAsync();
        }

        // GET: api/chatsession/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatSession>> GetChatSession(int id)
        {
            var chatSession = await _context.ChatSessions.FindAsync(id);

            if (chatSession == null)
            {
                return NotFound();
            }

            return chatSession;
        }

        // POST: api/chatsession
        [HttpPost]
        public async Task<ActionResult<ChatSession>> PostChatSession(ChatSession chatSession)
        {
            _context.ChatSessions.Add(chatSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatSession), new { id = chatSession.SessionId }, chatSession);
        }

        // Not necessary
        // PUT: api/ChatSessions/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutChatSession(int id, ChatSession chatSession)
        // {
        //     if (id != chatSession.SessionId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(chatSession).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ChatSessionExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // DELETE: api/ChatSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatSession(int id)
        {
            var chatSession = await _context.ChatSessions.FindAsync(id);
            if (chatSession == null)
            {
                return NotFound();
            }

            _context.ChatSessions.Remove(chatSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatSessionExists(int id)
        {
            return _context.ChatSessions.Any(e => e.SessionId == id);
        }
    }
}
