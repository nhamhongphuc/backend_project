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
    [Route("api/OrderDetail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private WebContext _context;
        public OrderDetailController(WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            var orderdetails = await (from A in _context.OrderDetails
                              select A).ToListAsync();
            if (orderdetails != null)
            {
                return orderdetails;
            }
            else
            {
                return (IEnumerable<OrderDetail>)NotFound();
            }
        }

     //Get by OrderID + productID
        [HttpGet]
        [Route("GetbyID/{id1?}/{id2?}")]
        public async Task<ActionResult<OrderDetail>> GetbyID(int id1, int id2)
        {

            var orderdetail = await (from c in _context.OrderDetails
                                  where c.OrderID.Equals(id1) && c.ProductID.Equals(id2)
                                  select c).FirstOrDefaultAsync();
            if (orderdetail != null)
            {
                return Ok(orderdetail);
            }
            else
            {
                return NotFound();
            }
        }

        

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<OrderDetail>> Post(OrderDetail orderdetail)
        {
            if (orderdetail != null)
            {
                var order = _context.Orders.Find(orderdetail.OrderID);
                if (order != null)
                {
                    order.Total += orderdetail.Money;
                }
               
                _context.OrderDetails.Add(orderdetail);
                await _context.SaveChangesAsync();
                return orderdetail;
            }
            else
            {
                return NoContent();
            }
        }

        //Put OrderID + ProductID
        [HttpPut]
        [Route("Put/{id1?}/{id2?}")]
        public async Task<ActionResult<OrderDetail>> Put(int id1,int id2, OrderDetail new_add)
        {
            if (id1 != new_add.OrderID && id2 != new_add.ProductID)
            {
                return BadRequest();
            }

            var orderdetail = _context.OrderDetails.Find(id1,id2);
            if (orderdetail != null)
            {
                var order = _context.Orders.Find(id1);
                if (order != null)
                {
                    order.Total = order.Total - orderdetail.Money + new_add.Money;
                }
                orderdetail.Capacity = new_add.Capacity;
                orderdetail.Money = new_add.Money;
                await _context.SaveChangesAsync();
                return orderdetail;
            }
            else
            {
                return NotFound();
            }
        }

          //Delete OrderID + productID
        [HttpDelete]
        [Route("Delete/{id1?}/{id2?}")]
        public async Task<ActionResult<OrderDetail>> Delete(int id1, int id2)
        {
            var orderdetail = _context.OrderDetails.Find(id1,id2);
            if (orderdetail != null)
            {
                var order = _context.Orders.Find(id1);
                if (order != null)
                {
                    order.Total = order.Total - orderdetail.Money;
                }
                _context.OrderDetails.Remove(orderdetail);
                await _context.SaveChangesAsync();
                return orderdetail;
            }
            else
            {
                return NotFound();
            }
        }
    }
}