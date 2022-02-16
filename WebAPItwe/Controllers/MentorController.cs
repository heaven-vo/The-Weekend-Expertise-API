using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/mentors")]
    [ApiController]
    [Produces("application/json")]
    public class MentorController : ControllerBase
    {
        private readonly InMentorRepository mentorRepository;

        public MentorController(InMentorRepository mentorRepository)
        {
            this.mentorRepository = mentorRepository;
        }

        //GET: api/v1/mentor?pageIndex=1&pageSize=3
        /// <summary>
        /// Get list all mentors with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll(int pageIndex, int pageSize)
        {
            return Ok( await mentorRepository.GetAll(pageIndex, pageSize));
        }

        //GET: api/v1/mentors/{id}
        /// <summary>
        /// Get a mentor by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var mentor = await mentorRepository.GetById(id);
            if (mentor == null)
                return NotFound();
            return Ok(mentor);
        }

        //GET: api/v1/mentors/sorting?sort_by=xxx&order_by=xxx
        /// <summary>
        /// Sort list mentor by price
        /// </summary>
        [HttpGet("sorting")]
        public async Task<IEnumerable<MentorModel>> SortbyPrice()
        {
            return await mentorRepository.SortByPrice();
        }

        //GET: api/v1/mentors/byName?name=xxx
        /// <summary>
        /// Search mentor by name
        /// </summary>
        [HttpGet("byName")]
        public async Task<ActionResult> FindByName(string name)
        {
            var mentor = await mentorRepository.FindByName(name);
            if (!mentor.Any())
            {
                return NotFound();
            }
            return Ok(await mentorRepository.FindByName(name));
        }
        //GET: api/v1/mentors/filter?major=xxx
        /// <summary>
        /// Filter by major name
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult> Filter(string major)
        {
            var listMentor = await mentorRepository.Filter(major);
            return (listMentor.Any()) ? Ok(listMentor) : NotFound();
        }
    }
}
