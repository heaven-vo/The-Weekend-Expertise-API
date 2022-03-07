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
    [Route("api/v1/admin/sessions")]
    [ApiController]
    public class AdminSessionController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public AdminSessionController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        /// <summary>
        /// Get list all Session with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions(int pageIndex, int pageSize)
        {

            try
            {
                var session = await (from c in _context.Sessions
                                     join cm in _context.Mentors on c.MentorId equals cm.Id
                                     join cf in _context.Cafes on c.CafeId equals cf.Id

                                     select new
                                     {
                                         Id = c.Id,
                                         MaxPerson = c.MaxPerson,
                                         SubjectName = c.SubjectName,
                                         MentorName = c.MentorName,
                                         Price = c.Price,
                                         Street = cf.Street,
                                         Fullname = cm.Fullname,
                                         Distric = cf.Distric,
                                         Date = c.Date,
                                         Phone = cm.Phone,
                                         Status = c.Status

                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(session);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // GET: api/Sessions/5
        /// <summary>
        /// Get a Session by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(string id)
        {
            var session = await (from c in _context.Sessions

                                 join cm in _context.Mentors on c.MentorId equals cm.Id
                                 join cf in _context.Cafes on c.CafeId equals cf.Id
                                 where c.Id == id

                                 select new
                                 {

                                     Id = c.Id,
                                     MaxPerson = c.MaxPerson,
                                     SubjectName = c.SubjectName,
                                     MentorName = c.MentorName,
                                     Price = c.Price,
                                     Street = cf.Street,
                                     Fullname = cm.Fullname,
                                     Distric = cf.Distric,
                                     Date = c.Date,
                                     Phone = cm.Phone,
                                     Status = c.Status

                                 }).ToListAsync();


            if (!session.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(session);
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(string id, Session session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            _context.Sessions.Add(session);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SessionExists(session.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(string id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(string id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
