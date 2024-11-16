using CRUD_WEB_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody]  User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(user);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            var userData = await context.Users.FindAsync(id);
            if(userData is null)
            {
                return NotFound();
            }
            userData.Name = user.Name;
            userData.Phone = user.Phone;
            userData.Email = user.Email;
            await context.SaveChangesAsync();
            return Ok(userData);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var userData = await context.Users.FindAsync(id);
            if (userData is null)
            {
                return NotFound();
            }
            context.Users.Remove(userData);
            await context.SaveChangesAsync();
            return Ok(userData);
        }
    }
}
