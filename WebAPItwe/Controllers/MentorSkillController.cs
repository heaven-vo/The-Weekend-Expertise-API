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
    [Route("api/[controller]")]
    [ApiController]
    public class MentorSkillController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public MentorSkillController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/v1/mentorSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentorSkill>>> GetMentorSkills(int pageIndex, int pageSize)
        {
            try
            {
                var result = await (from c in _context.MentorSkills
                                    select new
                                    {
                                        Id = c.Id,
                                        SkillId = c.SkillId,
                                        MentorId = c.MentorId

                                    }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // GET: aapi/v1/mentorSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorSkill>> GetMentorSkill(string id)
        {
            var result = await (from c in _context.MentorSkills
                                where c.Id == id
                                select new
                                {
                                    c.Id,
                                    c.SkillId,
                                    c.MentorId
                                }).ToListAsync();


            if (!result.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(result);
        }

        //Get: api/v1/mentorSkills/mentorId
        [HttpGet("mentorId")]
        public async Task<ActionResult<MentorSkill>> GetByMentorId(string mentorId)
        {
            try
            {
                var result = await (from MentorSkill in _context.MentorSkills
                                    where MentorSkill.MentorId.Contains(mentorId)    // search gần đúng
                                    select new
                                    {
                                        MentorSkill.Id,
                                        MentorSkill.SkillId,
                                        MentorSkill.MentorId
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
        // Put: api/v1/mentorSkills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentorSkill(string id, MentorSkill mentorSkill)
        {
            try
            {
                var result = _context.MentorSkills.Find(id);
                result.Id = mentorSkill.Id;
                result.MentorId = mentorSkill.MentorId;
                result.SkillId = mentorSkill.SkillId;
                await _context.SaveChangesAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // POST: api/v1/mentorSkills
        [HttpPost]
        public async Task<ActionResult<MentorSkill>> PostMentorSkill(MentorSkill mentorSkill)
        {
            try
            {
                var result = new MentorSkill();
                result.Id = mentorSkill.Id;
                result.MentorId = mentorSkill.MentorId;
                result.SkillId = mentorSkill.SkillId;

                _context.MentorSkills.Add(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // DELETE: api/v1/mentorSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentorSkill(string id)
        {
            var reslut = await _context.MentorSkills.FindAsync(id);
            if (reslut == null)
            {
                return NotFound();
            }
            _context.MentorSkills.Remove(reslut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
