using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private WebContext _context;
        public ProductsController(WebContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var products = await (from p in _context.Products
                                  select p).ToListAsync();
            if (products != null)
            {
                return products;
            }
            else
            {
                return (IEnumerable<Product>)NoContent();
            }
        }

        //Get by ID
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Product>> GetbyID(int id)
        {

            var product = await (from p in _context.Products
                                  where p.ProductID.Equals(id)
                                  select p).FirstOrDefaultAsync();
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        //Get product with the most purchases
        //[HttpGet]
        //[Route("GetByPurchase")]
        //public async Task<IEnumerable<Product>> GetByPurchase()
        //{

        //    var products = await (from p in _context.Products
        //                         join d in _context.OrderDetails on p.ProductID equals d.ProductID
        //                         group d by d.ProductID into newgr
        //                         orderby newgr.Count() descending
        //                         select newgr).ToListAsync();
        //    if (products != null)
        //    {
        //        return (IEnumerable<Product>)products;
        //    }
        //    else
        //    {
        //        return (IEnumerable<Product>)NotFound();
        //    }
        //}

        //Loot 
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        //Get by name
        [HttpGet]
        [Route("GetByName/{name?}")]
        public async Task<IEnumerable<Product>> GetbyName(string name)
        {

            var products = await (from p in _context.Products
                                 where p.ProductName.Contains(name)
                                 select p).ToListAsync();
            if (products != null)
            {
                return products;
            }
            else
            {
                return (IEnumerable<Product>)NotFound();
            }
        }
        
        //Get with producttype
        [HttpGet]
        [Route("GetByType/{type?}")]
        public async Task<IEnumerable<Product>> GetByType(string type)
        {
            var list = await (from p in _context.Products
                              join t in _context.ProductTypes on p.ProductTypeID equals t.ProductTypeID
                              where t.ProductTypeName.Equals(type)
                              select p).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return (IEnumerable<Product>)NotFound();
            }
        }

        //Get with supplier
        [HttpGet]
        [Route("GetBySupplier/{supplier?}")]
        public async Task<IEnumerable<Product>> GetBySupplier(string supplier)
        {
            var list = await (from p in _context.Products
                              join s in _context.Suppliers on p.SupplierID equals s.SupplierID
                              where s.SupplierName.Equals(supplier)
                              select p).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return (IEnumerable<Product>)NotFound();
            }
        }


        //Get with supplier and producttype
        [HttpGet]
        [Route("GetByFilter/{supplier?}/{type?}")]
        public async Task<IEnumerable<Product>> GetByFilter(int supplier, int type)
        {
            var list = await (from p in _context.Products
                              join s in _context.Suppliers on p.SupplierID equals s.SupplierID
                              join t in _context.ProductTypes on p.ProductTypeID equals t.ProductTypeID
                              where s.SupplierID == (supplier) && t.ProductTypeID == (type)
                              select p).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return (IEnumerable<Product>)NotFound();
            }
        }
        //Post
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Product>> Put(int id, Product new_product)
        {
            if (id != new_product.ProductID)
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.ProductName = new_product.ProductName;
                product.ProductTypeID = new_product.ProductTypeID;
                product.SupplierID = new_product.SupplierID;
                product.Price = new_product.Price;
                product.img_URL = new_product.img_URL;
                await _context.SaveChangesAsync();
                return Ok(new_product);
            }
            else
            {
                return NotFound();
            }
        }

        //Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
