using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
   
    [Route("api/Account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private WebContext _context;
        public AccountsController(WebContext context)
        {
            _context = context;
        }
    
        //Get
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Account>> GetAll()
        {
            var accounts = await (from A in _context.Accounts
                                  select A).ToListAsync();
            if (accounts != null)
            {
                return accounts;
            }
            else
            {
                return (IEnumerable<Account>)NotFound();
            }
        }

        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Account>> GetbyID(string id)
        {
            var accounts = await (from A in _context.Accounts
                                  where A.AccountID.Equals(id)
                                  select A).FirstOrDefaultAsync();
            if (accounts != null)
            {
                return accounts;
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Account>> Post(Account account)
        {         
            if (account != null)
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return account;
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Account>> Put(string id, Account new_Acc)
        {
            if (id != new_Acc.AccountID)
            {
                return BadRequest();
            }
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {
                acc.AccountPassword = new_Acc.AccountPassword;
                acc.UserID = new_Acc.UserID;
                acc.IsAdmin = new_Acc.IsAdmin;
                acc.IsActive = new_Acc.IsActive;
                acc.CreatedDate = new_Acc.CreatedDate;
                await _context.SaveChangesAsync();
                return acc;
            }
            else
            {
                return NotFound();
            }
        }

        //Active
        [HttpPut]
        [Route("Active/{id?}")]
        public async Task<ActionResult<Account>> Active(string id)
        {           
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {              
                acc.IsActive = true;                
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //Inactive
        [HttpPut]
        [Route("Inactive/{id?}")]
        public async Task<ActionResult<Account>> Inactive(string id)
        {
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {
                acc.IsActive = false;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //Set admin
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("SetAdmin/{id?}")]
        public async Task<ActionResult<Account>> SetAdmin(string id)
        {
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {
                acc.IsAdmin = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<Account>> Delete(string id)
        {
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {
                _context.Accounts.Remove(acc);
                await _context.SaveChangesAsync();
                return acc;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Account>> Login(Account tmp)
        {
            var Acc = await (from A in _context.Accounts
                             where A.AccountID == tmp.AccountID && A.AccountPassword == tmp.AccountPassword
                             select A).FirstOrDefaultAsync();
            if (Acc != null)
            {
                return Ok(Acc);
            }
            else
            {
                return NotFound();
            }    
        }
    }

 
}
