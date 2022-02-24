using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItwe.Entities;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/majors")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public MajorController(dbEWTContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all major
        /// </summary>
        // GET: api/v1/majors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MajorModel>>> GetMajors()
        {
            return await _context.Majors.Select(x => new MajorModel { Id = x.Id, Name = x.Name, Status = x.Status }).ToListAsync();
        }

        // GET: api/v1/majors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MajorModel>> GetMajor(string id)
        {
            var major = await _context.Majors.Select(x => new MajorModel { Id = x.Id, Name = x.Name, Status = x.Status }).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (major == null)
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(major);
        }

        //Get: api/v1/majors/name?name=xxxx
        [HttpGet("name")]
        public async Task<ActionResult<Major>> GetByName(string name)
        {
            try
            {
                var result = await (from Major in _context.Majors
                                    where Major.Name.Contains(name)    // search gần đúng
                                    select new
                                    {
                                        Major.Id,
                                        Major.Name,
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

        // PUT: api/v1/majors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMajor(string id, MajorModel major)
        {
            if (id != major.Id)
            {
                return BadRequest();
            }
            Major en = new Major();
            en.Id = major.Id;
            en.Name = major.Name;
            en.Status = major.Status;    

            try
            {
                _context.Entry(en).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(id))
                {
                    return NotFound(new { StatusCode = 404, message = "The major is not existed." });
                }
                else
                {
                    throw;
                }
            }

            return Ok(en);
        }

        // POST: api/v1/majors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Major>> PostMajor(MajorModel major)
        {
            var en = new Major();
            en.Id = major.Id;
            en.Name = major.Name;
            en.Status = major.Status;
            _context.Majors.Add(en);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MajorExists(major.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMajor", new { id = major.Id }, major);
        }

        // DELETE: api/Major/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajor(string id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }

            _context.Majors.Remove(major);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MajorExists(string id)
        {
            return _context.Majors.Any(e => e.Id == id);
        }
    }
}
