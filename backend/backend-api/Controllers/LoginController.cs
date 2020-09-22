using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETCore_API_REST_Template.Auth;
using ASP.NETCore_API_REST_Template.Data;
using ASP.NETCore_API_REST_Template.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP.NETCore_API_REST_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Login
        [HttpPost]
        public ActionResult PostLogin(BodyLogin body)
        {
            User user = _context.Users.ToList().Find(x => x.Username.Equals(body.Username));
            if (TokenManager.VerifyPassword(body.Password, user.Hash, user.Salt))
                return Ok(TokenManager.generateToken(body.Username));
            else
                return Unauthorized();
        }
    }

    public class BodyLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}