using BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("api/Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private WebContext _context;
        public AddressController(WebContext context)
        {
            _context = context;
        }


        //Get
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Address>> GetAll()
        {
            var Adds = await (from A in _context.Addresses
                              select A).ToListAsync();
            if (Adds != null)
            {
                return Adds;
            }
            else
            {
                return (IEnumerable<Address>)NotFound();
            }
        }

        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Address>> GetbyID(int id)
        {
            var Add = await (from A in _context.Addresses
                             where A.AddressID.Equals(id)
                              select A).FirstOrDefaultAsync();
            if (Add != null)
            {
                return Add;
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Address>> Post(Address add)
        {
            if (add != null)
            {
                _context.Addresses.Add(add);
                await _context.SaveChangesAsync();
                return add;
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Address>> Put(int id, Address new_add)
        {
            if (id != new_add.AddressID)
            {
                return BadRequest();
            }

            var add = _context.Addresses.Find(id);
            if (add != null)
            {
                add.AccountID = new_add.AccountID;
                add.Diachi = new_add.Diachi;
                await _context.SaveChangesAsync();
                return add;
            }
            else
            {
                return NotFound();
            }
        }

        //Delete
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<Address>> Delete(int id)
        {
            var add = _context.Addresses.Find(id);
            if (add != null)
            {
                _context.Addresses.Remove(add);
                await _context.SaveChangesAsync();
                return add;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
