using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(DataContext Context) : ControllerBase
    {
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await Context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]  
        public async Task <ActionResult<AppUser>> GetUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return Ok(user);  
        }
    }
}