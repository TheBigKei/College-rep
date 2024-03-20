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
    public class UserController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpGet("login={log}&pass={pass}")]
        public async Task<IActionResult> GetUser([FromRoute] string log, [FromRoute] string pass)
        {
            var user = await _context.Users.Where(u => u.Login == log && u.Password == pass).FirstOrDefaultAsync();

            if (user == null)
                return BadRequest("User not found");

            return Ok(user);
        }

        [HttpPost("reg")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO newUser) 
        {
            var userIsExist = await _context.Users
                .Where(u => u.Login == newUser.Login && u.Password == newUser.Password)
                .AnyAsync();

            if (userIsExist)
                return BadRequest("User already exist");

            User user = new()
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Patronymic = newUser.Patronymic,
                Login = newUser.Login,
                Password = newUser.Password,
                PermissionId = 1
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User has been added!");
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id) 
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
                return BadRequest("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User has been deleted!");
        }
    }
}
