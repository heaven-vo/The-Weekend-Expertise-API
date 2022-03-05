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
    [Route("api/v1/admin/skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public SkillController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/v1/skills
        /// <summary>
        /// Get list all Skill with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkills(int pageIndex, int pageSize)
        {
            try
            {
                var session = await (from c in _context.Skills
                                     join ms in _context.MentorSkills on c.Id equals ms.SkillId
                                     join mt in _context.Mentors on ms.MentorId equals mt.Id
                                     join mm in _context.MentorMajors on mt.Id equals mm.MentorId
                                     join m in _context.Majors on  mm.MajorId equals m.Id

                                     select new
                                     {
                                         Id = c.Id,
                                         Fullname = mt.Fullname,
                                         Major = m.Name,
                                         Skill = c.Name

                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


                return Ok(session);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        // GET: api/v1/skills/5
        /// <summary>
        /// Get a Skill by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillModel>> GetSkill(string id)
        {
            var session = await (from c in _context.Skills
                                 join ms in _context.MentorSkills on c.Id equals ms.SkillId
                                 join mt in _context.Mentors on ms.MentorId equals mt.Id
                                 join mm in _context.MentorMajors on mt.Id equals mm.MentorId
                                 join m in _context.Majors on mm.MajorId equals m.Id
                                 where c.Id == id

                                 select new
                                 {
                                     Id = c.Id,
                                     Fullname = mt.Fullname,
                                     Major = m.Name,
                                     Skill = c.Name

                                 }).ToListAsync();


            if (!session.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(session);
        }

        //GET: api/v1/cafe/byName?name=xxx
        /// <summary>
        /// Search Skill by name
        /// </summary>
        [HttpGet("name")]
        public async Task<ActionResult<Skill>> GetByName(string name)
        {
            try
            {
                var session = await (from c in _context.Skills
                                     join ms in _context.MentorSkills on c.Id equals ms.SkillId
                                     join mt in _context.Mentors on ms.MentorId equals mt.Id
                                     join mm in _context.MentorMajors on mt.Id equals mm.MentorId
                                     join m in _context.Majors on mm.MajorId equals m.Id
                                     where c.Name.Contains(name)

                                     select new
                                     {
                                         Id = c.Id,
                                         Fullname = mt.Fullname,
                                         Major = m.Name,
                                         Skill = c.Name

                                     }).ToListAsync();
                if (!session.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "Name is not found!" });
                }
                return Ok(session);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        /// <summary>
        /// Update Skill by Id
        /// </summary>
        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(string id, SkillModel skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }
            Skill en = new Skill();
            en.Id = skill.Id;
            en.Name = skill.Name;

            try
            {
                _context.Entry(en).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Dell biet :)");
                }
            }

            return Ok(skill);
        }

        /// <summary>
        /// Create Skill by Id
        /// </summary>
        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillModel>> PostSkill(SkillModel skill)
        {
            Skill en = new Skill();
            en.Id = skill.Id;
            en.Name = skill.Name;
            
            try
            {
                _context.Skills.Add(en);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SkillExists(skill.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(string id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
