using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public SubjectsController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/v1/subjects
        /// <summary>
        /// Get list all Subject with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects(int pageIndex, int pageSize)
        {
            try
            {
                var subject = await (from c in _context.Subjects
                                     select new
                                     {
                                         Id = c.Id,
                                         Name = c.Name,
                                         MajorId = c.MajorId

                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        /// <summary>
        /// Get a Subject by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(string id)
        {
            var subject = await (from c in _context.Subjects
                                 where c.Id == id
                                 select new
                                 {
                                     c.Id,
                                     c.Name,
                                     c.MajorId

                                 }).FirstOrDefaultAsync();


            if (subject == null)
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(subject);
        }

        /// <summary>
        /// Search Skill by name
        /// </summary>
        [HttpGet("name")]
        public async Task<ActionResult<Subject>> GetByName(string name)
        {
            try
            {
                var result = await (from Subject in _context.Subjects
                                    where Subject.Name.Contains(name)    // search gần đúng
                                    select new
                                    {
                                        Subject.Id,
                                        Subject.Name,
                                        Subject.MajorId
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
        /// Search Subject by majorId
        /// </summary>
        [HttpGet("MajorId")]
        public async Task<ActionResult<Subject>> GetByMajorId(string majorId)
        {
            try
            {
                var result = await (from Subject in _context.Subjects
                                    where Subject.MajorId == majorId   // search gần đúng
                                    select new
                                    {
                                        Subject.Id,
                                        Subject.MajorId,
                                        Subject.Name
                                    }).ToListAsync();
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
        // PUT: api/v1/subjects/5
        // PUT: api/Subjects/5
        /// <summary>
        /// Update Subject by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(string id, Subject subject)
        {
            try
            {
                var result = _context.Subjects.Find(id);
                result.Id = subject.Id;
                result.Name = subject.Name;
                result.MajorId = subject.MajorId;
                await _context.SaveChangesAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // POST: api/v1/subjects
        /// <summary>
        /// Create Subject by Id
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            try
            {
                var result = new Subject();

                result.Id = subject.Id;
                result.Name = subject.Name;
                result.MajorId = subject.MajorId;

                _context.Subjects.Add(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // DELETE: api/v1/subjects/5
        /// <summary>
        /// Delete Subject by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
