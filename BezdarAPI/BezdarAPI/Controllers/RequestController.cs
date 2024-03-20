using BezdarAPI.DataContext.DTO;
using BezdarAPI.DataContext.Model;
using BezdarAPI.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BezdarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetRequestById([FromRoute] int id)
        {
            var request = await _context.Requests.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (request == null)
                return BadRequest("Request not found");

            return Ok(request);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRequest([FromBody] RequestDTO newRequest)
        {
            var requestIsExist = await _context.Requests.Where(r => r.ShopId == newRequest.ShopId && r.UserId == newRequest.UserId).AnyAsync();

            if (requestIsExist)
                return BadRequest("Request already exist");

            Request shop = new()
            {
                
            };

            await _context.Requests.AddAsync(shop);
            await _context.SaveChangesAsync();

            return Ok("Request has been added!");
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteRequest([FromRoute] int id)
        {
            var request = await _context.Requests.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (request == null)
                return BadRequest("Request not found");

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok("Request has been deleted!");
        }
    }
}
