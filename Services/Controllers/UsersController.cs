using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using Services.Models;
using Services.Helpers;
using System.Security.Claims;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly moptaskDBContext _context;

        public UsersController(IUsersService usersService, moptaskDBContext context)
        {
            this._usersService = usersService;
            this._context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            //User userInDB = await _usersService.GetUserById(id);
            //if (userInDB == null)
            //{
            //    return NotFound();
            //}
            //return Ok(await _usersService.GetUserById(id));

            if (id < 0)
            {
                id = SharedHelpers.GetUserIdFromToken(User.Identity as ClaimsIdentity);
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _usersService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
    }
}
