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
    public class AdminSkillController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public AdminSkillController(dbEWTContext context)
        {
            _context = context;
        }

        // GET: api/v1/skills
        /// <summary>
        /// Get list all Skill with pagination
        /// </summary>
        [HttpGet("skill")]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkills(int pageIndex=1, int pageSize =5)
        {
            try
            {
                var session = await (from c in _context.Skills
                                     select new
                                     {
                                         Id = c.Id,
                                         Name = c.Name,

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
                                 where c.Id == id
                                 select new
                                 {
                                     Id = c.Id,
                                     Name = c.Name

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
                                     where c.Name.Contains(name)

                                     select new
                                     {
                                         Id = c.Id,
                                         Name = c.Name,

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
            try
            {
                var result = _context.Skills.Find(id);
                result.Id = skill.Id;
                result.Name = skill.Name;

                await _context.SaveChangesAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        /// <summary>
        /// Create Skill by Id
        /// </summary>
        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillModel>> PostSkill(SkillModel skill)
        {
            try
            {
                var result = new Skill();

                result.Id = skill.Id;
                result.Name = skill.Name;

                _context.Skills.Add(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }

        /// <summary>
        /// Delete skill by Id
        /// </summary>
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
        /// <summary>
        /// Sort skill by name
        /// </summary>
        [HttpGet("sorting/name")]
        public async Task<ActionResult<Skill>> SortByName(int pageIndex = 1, int pageSize = 5)
        {
            try
            {
                var listSkill = await _context.Skills.OrderBy(c => c.Name).Select(c => new Skill
                {
                    Id = c.Id,
                    Name = c.Name
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(listSkill);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// update status skill by id
        /// </summary>
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutSkill(string id)
        {
            try
            {
                var skill = _context.Skills.Find(id);
                if(skill.Status == true)
                {
                    skill.Status = false;
                }
                else
                {
                    skill.Status = true;
                }
                await _context.SaveChangesAsync();
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
    }
}
