using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{

    public class UsersController(DataContext Context) : BaseApiController
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

        //[HttpPost]
        //public async Task<ActionResult<AppUser>> CreateUser(AppUser appUser)
        //{
        //    Context.Users.Add(appUser);
        //    await Context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetUser), new { id = appUser.ID }, appUser);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, AppUser appUser)
        //{
        //    var user = await Context.Users.FindAsync(id);
        //    if (user == null) return NotFound();

        //    user.UserName = appUser.UserName;
        //    user.PhoneNumber = appUser.PhoneNumber;
        //    user.Email = appUser.Email;
        //    user.Address = appUser.Address;

        //    Context.Users.Update(user);
        //    await Context.SaveChangesAsync();

        //    return Ok(new { message = "User updated successfully", user });
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null) return NotFound();

            Context.Users.Remove(user);
            await Context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully" });
        }
    }
}