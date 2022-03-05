using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;

namespace WebAPItwe.Controllers.Admin
{
    [Route("api/v1/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
         private readonly dbEWTContext _context;

        public AdminController(dbEWTContext context)
        {
            _context = context;
        }

         [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var caf = await (from c in _context.Cafes
                                 select new
                                 {
                                     Id = c.Id,
                                     name = c.Name,
                                     Image = c.Image,
                                     Street = c.Street,
                                     Distric = c.Distric,
                                     Description = c.Description
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
