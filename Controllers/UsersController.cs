using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;

namespace PR_103_2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PR_103_2019Context _context;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, PR_103_2019Context db)
        {
            _userService = userService;
            _context = db;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult RegisterUser([FromBody]UserDto userDto)
        {
            UserDto user;
            try
            {
                user = _userService.RegisterUser(userDto);
                if(user== null)
                {
                    return BadRequest("Korisnik nije mogao da se doda");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool UserExists(long id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
