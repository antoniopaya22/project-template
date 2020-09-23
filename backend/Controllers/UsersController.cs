using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NETCore_API_REST_Template.Data;
using ASP.NETCore_API_REST_Template.Model;
using ASP.NETCore_API_REST_Template.Auth;
using Newtonsoft.Json;

namespace ASP.NETCore_API_REST_Template.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //var token = Request.Headers["authorization"];
            //if (String.IsNullOrEmpty(token) || !TokenManager.validateToken(token))
            //    return Unauthorized();
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var token = Request.Headers["authorization"];
            if (String.IsNullOrEmpty(token) || !TokenManager.validateToken(token))
                return Unauthorized();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            var token = Request.Headers["authorization"];
            if (String.IsNullOrEmpty(token) || !TokenManager.validateToken(token))
                return Unauthorized();

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
        public async Task<ActionResult<User>> PostUser(BodyUser bodyUser)
        {
            HashSalt hashSalt = TokenManager.GenerateSaltedHash(bodyUser.Password);
            User user = new User()
            {
                Username = bodyUser.Username,
                Lastname = bodyUser.Lastname,
                Hash = hashSalt.Hash,
                Salt = hashSalt.Salt
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/:id
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var token = Request.Headers["authorization"];
            if (String.IsNullOrEmpty(token) || !TokenManager.validateToken(token))
                return Unauthorized();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class BodyUser
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}
