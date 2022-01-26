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
    [Route("api/SearchHistory")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {
        private WebContext _context;
        public SearchHistoryController(WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<SearchHistory>> GetAll()
        {
            var searchhistories = await (from A in _context.SearchHistories
                              select A).ToListAsync();
            if (searchhistories != null)
            {
                return searchhistories;
            }
            else
            {
                return (IEnumerable<SearchHistory>)NotFound();
            }
        }

     //Get by ID
        [HttpGet]
        [Route("GetbyID/{id?}")]
        public async Task<ActionResult<SearchHistory>> GetbyID(int id)
        {

            var searchhistory = await (from c in _context.SearchHistories
                                  where c.SearchHistoryID.Equals(id)
                                  select c).FirstOrDefaultAsync();
            if (searchhistory != null)
            {
                return Ok(searchhistory);
            }
            else
            {
                return NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<SearchHistory>> Post(SearchHistory searchhistory)
        {
            if (searchhistory != null)
            {
                _context.SearchHistories.Add(searchhistory);
                await _context.SaveChangesAsync();
                return searchhistory;
            }
            else
            {
                return NoContent();
            }
        }

        //Put
        [HttpPut]
        [Route("Put/{id?}")]
        public async Task<ActionResult<SearchHistory>> Put(int id, SearchHistory new_add)
        {
            if (id != new_add.SearchHistoryID)
            {
                return BadRequest();
            }

            var add = _context.SearchHistories.Find(id);
            if (add != null)
            {
                add.AccountID = new_add.AccountID;
                add.SearchContent = new_add.SearchContent;
                add.SearchDate = new_add.SearchDate;
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
        public async Task<ActionResult<SearchHistory>> Delete(int id)
        {
            var searchhistory = _context.SearchHistories.Find(id);
            if (searchhistory != null)
            {
                _context.SearchHistories.Remove(searchhistory);
                await _context.SaveChangesAsync();
                return searchhistory;
            }
            else
            {
                return NotFound();
            }
        }
    }
}