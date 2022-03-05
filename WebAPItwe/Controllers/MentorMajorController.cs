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
    [Route("api/v1/mentorMajors")]
    [ApiController]
    public class MentorMajorController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public MentorMajorController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/v1/mentorMajors
        /// <summary>
        /// Get list all MentorMajor with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentorMajor>>> GetMentorMajors(int pageIndex, int pageSize)
        {
            try
            {
                var result = await (from c in _context.MentorMajors
                                    select new
                                    {
                                        Id = c.Id,
                                        MajorId = c.MajorId,
                                        MentorId = c.MentorId

                                    }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // GET: api/v1/mentorMajors/5
        // GET: api/MentorMajors/5
        /// <summary>
        /// Get a MentorMajor by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorMajor>> GetMentorMajor(string id)
        {
            var result = await (from c in _context.MentorMajors
                                where c.Id == id
                                select new
                                {
                                    c.Id,
                                    c.MajorId,
                                    c.MentorId
                                }).FirstOrDefaultAsync();


            if (result == null)
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Search MentorMajor by mentorId
        /// </summary>
        [HttpGet("mentorId")]
        public async Task<ActionResult<MentorMajor>> GetByMentorId(string mentorId)
        {
            try
            {
                var result = await (from MentorMajor in _context.MentorMajors
                                    where MentorMajor.MentorId.Contains(mentorId)    // search gần đúng
                                    select new
                                    {
                                        MentorMajor.Id,
                                        MentorMajor.MajorId,
                                        MentorMajor.MentorId
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

        /// <summary>
        /// Search MentorSkill by majorId
        /// </summary>
        [HttpGet("MajorId")]
        public async Task<ActionResult<MentorMajor>> GetByMajorId(string majorId)
        {
            try
            {
                var result = await (from MentorMajor in _context.MentorMajors
                                    where MentorMajor.MajorId.Contains(majorId)    // search gần đúng
                                    select new
                                    {
                                        MentorMajor.Id,
                                        MentorMajor.MajorId,
                                        MentorMajor.MentorId
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
        // PUT: api/MentorMajors/5
        /// <summary>
        /// Update MentorMajor by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentorMajor(string id, MentorMajor mentorMajor)
        {
            try
            {
                var result = _context.MentorMajors.Find(id);
                result.Id = mentorMajor.Id;
                result.MentorId = mentorMajor.MentorId;
                result.MajorId = mentorMajor.MajorId;
                await _context.SaveChangesAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // POST: api/MentorMajors
        /// <summary>
        /// Create MentorMajor by Id
        /// </summary>54
        [HttpPost]
        public async Task<ActionResult<MentorMajor>> PostMentorMajor(MentorMajor mentorMajor)
        {
            try
            {
                var result = new MentorMajor();
                result.Id = mentorMajor.Id;
                result.MentorId = mentorMajor.MentorId;
                result.MajorId = mentorMajor.MajorId;

                _context.MentorMajors.Add(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // DELETE: api/MentorMajors/5
        /// <summary>
        /// Delete MentorMajor by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentorMajor(string id)
        {
            var result = await _context.MentorMajors.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.MentorMajors.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
