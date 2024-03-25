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

            // Получаем пользователя, чтобы получить название магазина
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.ShopTitle == newUser.ShopTitle);

            if (currentUser == null)
                return BadRequest("User with specified ShopTitle not found");

            User user = new()
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Patronymic = newUser.Patronymic,
                Login = newUser.Login,
                Password = newUser.Password,
                Email = newUser.Email,
                Salary = newUser.Salary,
                Phone = newUser.Phone,
                ShopTitle = currentUser.ShopTitle, // Используем название магазина из currentUser
                PermissionId = 1
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User has been added!");
        }

        [HttpGet("shopTitle={shopTitle}")]
        public async Task<IActionResult> GetUsersByShopTitle([FromRoute] string shopTitle)
        {
            var users = await _context.Users.Where(u => u.ShopTitle == shopTitle).ToListAsync();

            if (users == null || users.Count == 0)
                return BadRequest("Users not found for the specified shop title");

            return Ok(users);
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetUsersByShopTitle([FromRoute] int id)
        {
            var users = await _context.Users.Where(u => u.Id == id).ToListAsync();

            if (users == null || users.Count == 0)
                return BadRequest("Users not found for the specified id");

            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updatedUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return BadRequest("User not found");

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Patronymic = updatedUser.Patronymic;
            user.Login = updatedUser.Login;
            user.Password = updatedUser.Password;
            user.Email = updatedUser.Email;
            user.Salary = updatedUser.Salary;
            user.Phone = updatedUser.Phone;
            // Остальные поля обновления добавьте по мере необходимости

            await _context.SaveChangesAsync();

            return Ok("User has been updated!");
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
