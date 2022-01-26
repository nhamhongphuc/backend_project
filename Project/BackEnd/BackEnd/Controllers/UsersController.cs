using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private WebContext _context;
        public UsersController(WebContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<User>> GetAllUesr()
        {
            var Users = await (from U in _context.Users
                               select U).ToListAsync();

            if (Users != null)
            {           
                return Users;
            }
            else
            {
                return (IEnumerable<User>)NoContent();
            }
        }
        //Get by ID
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<User>> GetbyID(int id)
        {
            var User = await (from U in _context.Users
                              where U.UserID.Equals(id)
                               select U).FirstOrDefaultAsync();
            if (User != null)
            {
                return User;
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<User>> Put(int id, User new_user)
        {
            if (id != new_user.UserID)
            {
                return BadRequest();
            }
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.UserName = new_user.UserName;
                user.UserMail = new_user.UserMail;
                user.UserBirthdate = new_user.UserBirthdate;
                user.UserGender = new_user.UserGender;
                user.UserAddress = new_user.UserAddress;
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //Post procedure
        //Post
        [HttpPost]
        [Route("PostProcedure/{pass?}")]
   
        public async Task<ActionResult<User>> PostProcedure(string pass, User user)
        {
            if (user != null)
            {
                var tmp = _context.Database.ExecuteSqlInterpolated($"call CreateUser({user.UserName}, {user.UserMail}, {user.UserBirthdate}, {user.UserGender}, {user.UserAddress}, {pass})");
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
