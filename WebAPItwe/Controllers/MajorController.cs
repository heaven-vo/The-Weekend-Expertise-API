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

        // GET: api/Major
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MajorModel>>> GetMajors()
        {
            return await _context.Majors.Select(x => new MajorModel { Id = x.Id, Name = x.Name, Status = x.Status }).ToListAsync();
        }

        // GET: api/Major/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MajorModel>> GetMajor(string id)
        {
            var major = await _context.Majors.Select(x => new MajorModel { Id = x.Id, Name = x.Name, Status = x.Status }).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (major == null)
            {
                return NotFound();
            }

            return major;
        }

        // PUT: api/Major/5
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
            _context.Entry(en).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Major
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Major>> PostMajor(MajorModel major)
        {
            Major en = new Major();
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
