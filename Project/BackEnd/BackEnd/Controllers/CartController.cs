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
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private WebContext _context;
        public CartController(WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Cart>> GetAll()
        {
            var carts = await (from A in _context.Carts
                              select A).ToListAsync();
            if (carts != null)
            {
                return carts;
            }
            else
            {
                return (IEnumerable<Cart>)NotFound();
            }
        }

     //Get by ID
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Cart>> GetbyID(int id)
        {

            var cart = await (from c in _context.Carts
                                  where c.CartID.Equals(id)
                                  select c).FirstOrDefaultAsync();
            if (cart != null)
            {
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Cart>> Post(Cart cart)
        {
            if (cart != null)
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Cart>> Put(int id, Cart new_add)
        {
            if (id != new_add.CartID)
            {
                return BadRequest();
            }

            var add = _context.Carts.Find(id);
            if (add != null)
            {
                add.AccountID = new_add.AccountID;
                add.CartCapacity = new_add.CartCapacity;
                add.CartTotal = new_add.CartTotal;
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
        public async Task<ActionResult<Cart>> Delete(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetCartID/{id?}")]
        public Int32 GetCartID(string id)
        {
            var cart = Convert.ToInt32((from c in _context.Carts
                        where c.AccountID.Equals(id)
                        select c.CartID).FirstOrDefault());
            return Convert.ToInt32(cart);
        }
    }
}