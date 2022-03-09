using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;

namespace WebAPItwe.Controllers.Admin
{
    [Route("api/v1/admin/cafe")]
    [ApiController]
    public class AdminCafeController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public AdminCafeController(dbEWTContext context)
        {
            _context = context;
        }
        //GET: api/v1/cafe?pageIndex=1&pageSize=3
        /// <summary>
        /// Get list all cafe with pagination.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cafe>>> GetAll(int pageIndex = 1 , int pageSize = 5)
        {
            try
            {
                
                var caf = await (from c in _context.Cafes
                                 select new
                                 {
                                     Id = c.Id,
                                     name = c.Name,
                                     Image = c.Image,
                                     OpenTime = c.OpenTime,
                                     CloseTime = c.CloseTime,
                                     Price = c.Price,
                                     Street = c.Street,
                                     Distric = c.Distric,
                                     Description = c.Description,
                                     Rate = c.Rate,
                                     Status = c.Status,
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                return Ok(caf);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        //GET: api/v1/cafe/{id}
        /// <summary>
        /// Get a cafe by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cafe>> GetById(string id)
        {
            var cafe = await (from c in _context.Cafes
                              where c.Id == id
                              select new
                              {
                                  c.Id,
                                  c.Name,
                                  c.Image,
                                  c.OpenTime,
                                  c.CloseTime,
                                  c.Price,
                                  c.Street,
                                  c.Distric,
                                  c.Description,
                                  c.Rate
                              }).ToListAsync();


            if (!cafe.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(cafe);
        }
        //GET: api/v1/cafe/byName?name=xxx
        /// <summary>
        /// Search cafe by name
        /// </summary>
        [HttpGet("name")]
        public async Task<ActionResult<Cafe>> GetByName(string name)
        {
            try
            {
                var result = await (from Cafe in _context.Cafes
                                    where Cafe.Name.Contains(name)    // search gần đúng
                                    select new
                                    {
                                        Cafe.Id,
                                        Cafe.Name,
                                        Cafe.Image,
                                        Cafe.OpenTime,
                                        Cafe.CloseTime,
                                        Cafe.Price,
                                        Cafe.Street,
                                        Cafe.Distric,
                                        Cafe.Description,
                                        Cafe.Rate
                                    }
                               ).ToListAsync();
                if (!result.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "Name is not found!" });
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Update cafe by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCafe(string id, Cafe cafe)
        {
            try
            {
                var result = _context.Cafes.Find(id);
                result.Distric = cafe.Distric;
                result.Description = cafe.Description;
                result.Street = cafe.Street;
                result.Image = cafe.Image;
                result.OpenTime = cafe.OpenTime;
                result.CloseTime = cafe.CloseTime;
                result.Name = cafe.Name;
                result.Price = cafe.Price;
                result.Rate = cafe.Rate;

                await _context.SaveChangesAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Create cafe by Id
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Cafe>> PostCafe(Cafe cafe)
        {
            try
            {
                var result = new Cafe();

                result.Id = cafe.Id;
                result.Name = cafe.Name;
                result.Image = cafe.Image;
                result.CloseTime= cafe.CloseTime;
                result.OpenTime= cafe.OpenTime;
                result.Price= cafe.Price;
                result.Rate = cafe.Rate;
                result.Description = cafe.Description;
                result.Distric = cafe.Distric;
                result.Street = cafe.Street;

                _context.Cafes.Add(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }

        }
        /// <summary>
        /// Delete cafe by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(string id)
        {
            var caf = await _context.Cafes.FindAsync(id);
            if (caf == null)
            {
                return NotFound();
            }
            _context.Cafes.Remove(caf);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        /// <summary>
        /// Sort cafe by price
        /// </summary>
        [HttpGet("sorting/price")]
        public async Task<ActionResult<Cafe>> SortByPrice(int pageIndex = 1, int pageSize = 5)
        {
            try
            {
                var listCafe = await _context.Cafes.OrderBy(c => c.Price).Select(c => new Cafe
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    OpenTime = c.OpenTime,
                    CloseTime = c.CloseTime,
                    Price = c.Price,
                    Street = c.Street,
                    Distric = c.Distric,
                    Description = c.Description,
                    Rate = c.Rate
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(listCafe);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Sort cafe by name
        /// </summary>
        [HttpGet("sorting/name")]
        public async Task<ActionResult<Cafe>> SortByName(int pageIndex = 1, int pageSize = 5)
        {
            try
            {
                var listCafe = await _context.Cafes.OrderBy(c => c.Name).Select(c => new Cafe
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    OpenTime = c.OpenTime,
                    CloseTime = c.CloseTime,
                    Price = c.Price,
                    Street = c.Street,
                    Distric = c.Distric,
                    Description = c.Description,
                    Rate = c.Rate
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(listCafe);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
    }
}
