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
    [Route("api/CartDetail")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private WebContext _context;
        public CartDetailController (WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CartDetail>> GetAll()
        {
            var cartdetails = await (from A in _context.CartDetails
                              select A).ToListAsync();
            if (cartdetails != null)
            {
                return cartdetails;
            }
            else
            {
                return (IEnumerable<CartDetail>)NotFound();
            }
        }

        [HttpGet]
        [Route("GetwithAcc/{acc?}")]
        public async Task<IEnumerable<CartDetail>> GetwithAcc(string acc)
        {
            var cartdetails = await (from A in _context.CartDetails
                                     join C in _context.Carts on A.CartID equals C.CartID
                                     where C.AccountID.Equals(acc)
                                     select A).ToListAsync();
            if (cartdetails != null)
            {
                return cartdetails;
            }
            else
            {
                return (IEnumerable<CartDetail>)NotFound();
            }
        }

        //Get by cartID+productID
        [HttpGet]
        [Route("GetbyID/{id1?}/{id2?}")]
        public async Task<ActionResult<CartDetail>> GetbyID(int id1, int id2)
        {

            var cartdetail = await (from c in _context.CartDetails
                                  where c.CartID.Equals(id1) && c.ProductID.Equals(id2)
                                  select c).FirstOrDefaultAsync();
            if (cartdetail != null)
            {
                return Ok(cartdetail);
            }
            else
            {
                return NotFound();
            }
        }

        //Get by userid
        [HttpGet]
        [Route("GetbyAccountID/{id?}")]
        public async Task<ActionResult<object>> GetbyAccountID(string id)
        {
            List<object> list = new List<object>();
            var cartdetails = await (from d in _context.CartDetails
                                    join c in _context.Carts on d.CartID equals c.CartID
                                    where c.AccountID.Equals(id) 
                                    select d).ToListAsync();

            foreach (var item in cartdetails)
            {
                var tmp = (from p in _context.Products
                           where p.ProductID.Equals(item.ProductID)
                           select p).FirstOrDefault();
                list.Add(new
                {
                    ProductID = item.ProductID,
                    CartID = item.CartID,
                    Capacity = item.Capacity,
                    Money = item.Money,
                    AddDate = item.AddDate,
                    ProductName = tmp.ProductName,
                    Price = tmp.Price,
                    img_URL = tmp.img_URL
                });
            }    
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }
        //Convert cart details to order detail
        [HttpPost]
        [Route("Convert/{id?}")]
        public async Task<ActionResult> Convert(string id)
        {
            var cartdetails = await (from d in _context.CartDetails
                                     join c in _context.Carts on d.CartID equals c.CartID
                                     where c.AccountID.Equals(id)
                                     select d).ToListAsync();

            var cart = await (from c in _context.Carts
                              where c.AccountID.Equals(id)
                              select c).FirstOrDefaultAsync();

            var add = await (from d in _context.Addresses                            
                             where d.AccountID.Equals(id)
                             select d).FirstOrDefaultAsync();

            double tong = 0;
            foreach (var i in cartdetails)
            {
                tong += i.Money;
            }
            Order tmp = new Order();
            tmp.AddressID = add.AddressID;
            tmp.AccountID = id;
            tmp.CreatedDate = DateTime.Today;
            tmp.Status = 2;
            tmp.Total = tong;
            if (tmp != null)
            {
                _context.Orders.Add(tmp);
                await _context.SaveChangesAsync();
            }
            var new_order = await (from o in _context.Orders
                                   where o.AccountID.Equals(id) && o.CreatedDate.Equals(tmp.CreatedDate) && o.Status.Equals(2) && o.Total.Equals(tong)
                                   select o).FirstOrDefaultAsync();
            foreach (var i in cartdetails)
            {
                OrderDetail tmp1 = new OrderDetail();
                tmp1.OrderID = new_order.OrderID;
                tmp1.ProductID = i.ProductID;
                tmp1.Capacity = i.Capacity;
                tmp1.Money = i.Money;
                _context.OrderDetails.Add(tmp1);
            }

            cart.CartTotal = 0;
            cart.CartCapacity = 0;
            foreach (var i in cartdetails)
            {
                _context.CartDetails.Remove(i);
            }
            await _context.SaveChangesAsync(); 

            if (new_order!=null)
            {            
                return Ok();
            }
            else
            {
                return NoContent();
            }    
        }

        

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<CartDetail>> Post(CartDetail cartdetail)
        {
            var tmp = (from d in _context.CartDetails                     
                       where d.CartID.Equals(cartdetail.CartID) && d.ProductID.Equals(cartdetail.ProductID)
                       select d).FirstOrDefault();
            if (tmp == null)
            {
                if (cartdetail != null)
                {
                    _context.CartDetails.Add(cartdetail);
                    var cart = _context.Carts.Find(cartdetail.CartID);
                    if (cart != null)
                    {
                        cart.CartCapacity += 1;
                        cart.CartTotal += cartdetail.Money;
                    }
                    await _context.SaveChangesAsync();
                    return Ok(cartdetail);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                tmp.Capacity += cartdetail.Capacity;
                tmp.Money += cartdetail.Money;             
                var cart = _context.Carts.Find(cartdetail.CartID);
                if (cart != null)
                {
                    cart.CartTotal += cartdetail.Money;
                }
                
                await _context.SaveChangesAsync();
                return Ok(tmp);
            }    
            
        }

        //Put CardID + ProductID
        [HttpPut]
        [Route("Put/{id1?}/{id2?}/")]
        public async Task<ActionResult<CartDetail>> Put(int id1,int id2 , CartDetail new_add)
        {
            if (id1 != new_add.CartID & id2 != new_add.ProductID)
            {
                return BadRequest();
            }

            var add = _context.CartDetails.Find(id1,id2);
            if (add != null)
            {
                var cart = _context.Carts.Find(id1);
                if (cart != null)
                {                   
                    cart.CartTotal = cart.CartTotal - add.Money + new_add.Money;
                }
                add.Capacity = new_add.Capacity;
                add.Money = new_add.Money;
                add.AddDate = new_add.AddDate;
                await _context.SaveChangesAsync();
                return Ok(add);
            }
            else
            {
                return NotFound();
            }
        }

          //Delete cart ID + Product ID
        [HttpDelete]
        [Route("Delete/{id1?}/{id2?}/")]
        public async Task<ActionResult<CartDetail>> Delete(int id1, int id2)
        {
            var cartdetail = _context.CartDetails.Find(id1,id2);
            if (cartdetail != null)
            {
                _context.CartDetails.Remove(cartdetail);
                var cart = _context.Carts.Find(cartdetail.CartID);
                if (cart != null)
                {
                    cart.CartCapacity -= 1;
                    cart.CartTotal -= cartdetail.Money;
                }              
                await _context.SaveChangesAsync();
                return cartdetail;
            }
            else
            {
                return NotFound();
            }
        }
    }
}