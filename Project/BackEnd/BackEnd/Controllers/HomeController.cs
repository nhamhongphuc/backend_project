using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private WebContext _context;
        public HomeController(WebContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Account model)
        {
            var user = await (from A in _context.Accounts
                              where A.AccountID == model.AccountID && A.AccountPassword == model.AccountPassword
                              select A).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            user.AccountPassword = "";
            var role = "";
            if (user.IsAdmin)
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }
            return new
            {
                user = user,
                role = role,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "You are Anonymous";
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

        [HttpGet]
        [Route("User")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<dynamic>> _User([FromBody] Account model)
        {
            var user = await(from A in _context.Accounts
                             where A.AccountID == model.AccountID && A.AccountPassword == model.AccountPassword
                             select A).FirstOrDefaultAsync();
            return new
            {
                user = user,
                role = "User"
            };
        }
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<dynamic>> _Admin([FromBody] Account model)
        {
            var user = await (from A in _context.Accounts
                              where A.AccountID == model.AccountID && A.AccountPassword == model.AccountPassword
                              select A).FirstOrDefaultAsync();
            return new
            {
                user = user,
                role = "Admin"
            };
        }
    }
}