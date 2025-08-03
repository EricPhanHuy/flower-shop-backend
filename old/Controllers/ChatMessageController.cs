using FlowerShop_BackEnd.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_BackEnd.Models;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop_BackEnd.Controllers
{
    [ApiController]
    [Route("api/chatmessage")]
    public class ChatMessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatMessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/chatmessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        // GET: api/chatmessage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // POST: api/chatmessage
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatMessage), new { id = chatMessage.MessageId }, chatMessage);
        }

        // Not Necessary
        // // PUT: api/chatmessage/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutChatMessage(int id, ChatMessage chatMessage)
        // {
        //     if (id != chatMessage.MessageId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(chatMessage).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ChatMessageExists(id))
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

        // DELETE: api/chatmessage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatMessageExists(int id)
        {
            return _context.ChatMessages.Any(e => e.MessageId == id);
        }
    }
}