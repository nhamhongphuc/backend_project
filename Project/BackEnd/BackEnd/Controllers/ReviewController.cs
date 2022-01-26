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
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private WebContext _context;
        public ReviewController(WebContext context)
        {
            _context = context;
        }
    //Get All
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Review>> GetAll()
        {
            var reviews = await (from A in _context.Reviews
                              select A).ToListAsync();
            if (reviews != null)
            {
                return reviews;
            }
            else
            {
                return (IEnumerable<Review>)NotFound();
            }
        }

     //Get by ProductID + AccountID
        [HttpGet]
        [Route("GetbyID/{id1?}/{id2?}")]
        public async Task<ActionResult<Review>> GetbyID(int id1, string id2)
        {

            var review = await (from c in _context.Reviews
                                  where c.ProductID.Equals(id1) && c.AccountID.Equals(id2)
                                  select c).FirstOrDefaultAsync();
            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
        }


        //Get by ProductID
        [HttpGet]
        [Route("GetbyProID/{id?}")]
        public async Task<IEnumerable<Review>> GetbyProID(int id)
        {
            var reviews = await (from A in _context.Reviews                              
                                 where A.ProductID.Equals(id)
                                 select A).ToListAsync();
            if (reviews != null)
            {
                return reviews;
            }
            else
            {
                return (IEnumerable<Review>)NotFound();
            }
        }

        //Post
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Review>> Post(Review review)
        {
            if (review != null)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return review;
            }
            else
            {
                return NoContent();
            }
        }

        //Put ProductID + AccountID(string_
        [HttpPut]
        [Route("Put/{id1?}/{id2?}")]
        public async Task<ActionResult<Review>> Put(int id1,string id2, Review new_add)
        {
            if (id1 != new_add.ProductID && !id2.Equals(id2))
            {
                return BadRequest();
            }

            var add = _context.Reviews.Find(id1,id2);
            if (add != null)
            {
                add.Ranking = new_add.Ranking;
                add.Comment=new_add.Comment;
                add.CreatedDate=new_add.CreatedDate;
                await _context.SaveChangesAsync();
                return add;
            }
            else
            {
                return NotFound();
            }
        }

          //Delete ProductID + AccountID
        [HttpDelete]
        [Route("Delete/{id1?}/{id2?}")]
        public async Task<ActionResult<Review>> Delete(int id1, string id2)
        {
            var review = _context.Reviews.Find(id1,id2);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return review;
            }
            else
            {
                return NotFound();
            }
        }
    }
}