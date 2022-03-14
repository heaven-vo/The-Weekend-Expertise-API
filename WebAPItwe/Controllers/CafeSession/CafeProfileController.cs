using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers.Cafe
{
    [Route("api/v1/cafe/sessions")]
    [ApiController]
    public class CafeProfileController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public CafeProfileController(dbEWTContext context)
        {
            _context = context;
        }

       // GET: api/v1/cafe? pageIndex = 1 & pageSize = 3
        /// < summary >
       /// Get list all cafe with pagination.
        /// </summary>
            [HttpGet]
        public async Task<ActionResult<IEnumerable<CafeModel>>> GetAll(int pageIndex= 1 , int pageSize = 5)
        {
            try
            {
                var caf = await (from c in _context.Cafes
                                 select new
                                 {
                                     Id = c.Id,
                                     name = c.Name,
                                     Image = c.Image,
                                     Price = c.Price,
                                     OpenTime = c.OpenTime,
                                     CloseTime = c.CloseTime,
                                     Street = c.Street,
                                     Distric = c.Distric,
                                     Description = c.Description,
                                     Rate = c.Rate

                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(caf);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
    }
}
