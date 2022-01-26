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
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private WebContext _context;
        public OrderController(WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await (from A in _context.Orders
                              select A).ToListAsync();
            if (orders != null)
            {
                return orders;
            }
            else
            {
                return (IEnumerable<Order>)NotFound();
            }
        }

     //Get by ID
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Order>> GetbyID(int id)
        {

            var order = await (from c in _context.Orders
                                  where c.OrderID.Equals(id)
                                  select c).FirstOrDefaultAsync();
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }

        //Update success
        [HttpPut]
        [Route("success/{id?}")]
        public async Task<ActionResult<Order>> Success(int id)
        {

            var order = await (from c in _context.Orders 
                               where c.OrderID.Equals(id)
                               select c).FirstOrDefaultAsync();
           
            if (order != null)
            {
                order.Status = 3;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }

        //Update cancel
        [HttpPut]
        [Route("cancel/{id?}")]
        public async Task<ActionResult<Order>> Cancel(int id)
        {

            var order = await (from c in _context.Orders
                               where c.OrderID.Equals(id)
                               select c).FirstOrDefaultAsync();

            if (order != null)
            {
                order.Status = 4;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order != null)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Order>> Put(int id, Order new_add)
        {
            if (id != new_add.OrderID)
            {
                return BadRequest();
            }

            var add = _context.Orders.Find(id);
            if (add != null)
            {
                add.AddressID = new_add.AddressID;
                add.AccountID = new_add.AccountID;
                add.CreatedDate = new_add.CreatedDate;
                add.Status = new_add.Status;
                add.Total = new_add.Total;
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
        public async Task<ActionResult<Order>> Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return order;
            }
            else
            {
                return NotFound();
            }
        }
    }
}