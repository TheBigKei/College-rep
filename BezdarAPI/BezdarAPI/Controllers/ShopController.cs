using BezdarAPI.DataContext;
using BezdarAPI.DataContext.DTO;
using BezdarAPI.DataContext.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BezdarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetShopById([FromRoute] int id) 
        {
            var shop = await _context.Shops.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (shop == null)
                return BadRequest("Shop not found");

            return Ok(shop);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShop([FromBody] ShopDTO newShop) 
        {
            var shopIsExist = await _context.Shops.Where(s => s.INN == newShop.INN && s.Title == newShop.Title).AnyAsync();

            if (shopIsExist)
                return BadRequest("Shop already exist");

            Shop shop = new() 
            {
                Title = newShop.Title,
                INN = newShop.INN
            };

            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();

            return Ok("Shop has been added!");
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteShop([FromRoute] int id)
        {
            var shop = await _context.Shops.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (shop == null)
                return BadRequest("User not found");

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return Ok("User has been deleted!");
        }
    }
}
