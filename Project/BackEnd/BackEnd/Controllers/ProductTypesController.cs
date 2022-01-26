using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/ProductType")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly WebContext _context;

        public ProductTypesController(WebContext context)
        {
            _context = context;
        }

        // GET: api/ProductTypes
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ProductType>> GetAll()
        {
            var list = await(from p in _context.ProductTypes
                        select p).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return (IEnumerable<ProductType>)NotFound();
            }
        }

        // GET: api/ProductTypes/5
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<ProductType>> GetbyID(int id)
        {
            var productType = await (from p in _context.ProductTypes
                                     where id == p.ProductTypeID
                                     select p).FirstOrDefaultAsync();

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

        // PUT: api/ProductTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<ProductType>> Put(int id, ProductType productType)
        {
            if (id != productType.ProductTypeID)
            {
                return BadRequest();
            }

            var tmp = _context.ProductTypes.Find(id);
            if (tmp != null)
            {
                tmp.ProductTypeName = productType.ProductTypeName;
                await _context.SaveChangesAsync();
                return tmp;
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/ProductTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            if (productType != null)
            {
                _context.ProductTypes.Add(productType);
                await _context.SaveChangesAsync();
                return productType;
            }
            else
            {
                return NoContent();
            }
          
        }

        // DELETE: api/ProductTypes/5
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<ProductType>> DeleteProductType(int id)
        {
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            else
            {
                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();
                return productType;
            }          
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.ProductTypeID == id);
        }
    }
}
