using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using static System.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/Supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private WebContext _context;
        public SupplierController(WebContext context)
        {
            _context = context;
        }

        //Get       
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {        
            var suppliers = await (from s in _context.Suppliers
                                  select s).ToListAsync();
            if (suppliers != null)
                return suppliers;
            else
                return (IEnumerable<Supplier>)NoContent();
        }

        //Get with id
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<Supplier>> GetSupplierbyID(int id)
        {
            var supplier = await (from s in _context.Suppliers
                                  where s.SupplierID.Equals(id)
                                  select s).FirstOrDefaultAsync();

            if (supplier != null)
                return supplier;
            else
                return NoContent();
        }
        //Post
        [Authorize(Roles = "Admin")]
        [Route("Post")]
        [HttpPost]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
            if (supplier != null)
            {
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                return supplier;
            }
            else
                return NoContent();          
        }
        //Put
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<Supplier>> Put(int id,Supplier new_supplier)
        {
            if (id != new_supplier.SupplierID)
            {
                return BadRequest();
            }
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                supplier.SupplierID = new_supplier.SupplierID;
                supplier.SupplierName = new_supplier.SupplierName;
                supplier.SupplierAddress = new_supplier.SupplierAddress;
                await _context.SaveChangesAsync();
                return supplier;
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
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync(); 
                return supplier;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
