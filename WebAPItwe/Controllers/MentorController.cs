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
    public class MentorController : ControllerBase
    {
        private readonly InMentorRepository mentorRepository;

        public MentorController(InMentorRepository mentorRepository)
        {
            this.mentorRepository = mentorRepository;
        }

        //GET: api/v1/mentor
        /// <summary>
        /// Get list all mentors
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<MentorModel>> GetAll()
        {
            return await mentorRepository.GetAll();
        }

        //GET: api/v1/mentors/{id}
        /// <summary>
        /// Get a mentor by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorModel>> GetById(string id)
        {
            return await mentorRepository.GetById(id);
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
        public async Task<ActionResult<IEnumerable<MentorModel>>> FindByName(string name)
        {
            var mentor = await mentorRepository.FindByName(name);
            if (!mentor.Value.Any())
            {
                return NotFound();
            }
            return await mentorRepository.FindByName(name);
        }
    }
}
