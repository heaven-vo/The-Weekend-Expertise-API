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

        //GET: api/v1/mentor?pageIndex=1&pageSize=3
        /// <summary>
        /// Get list all mentors with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll(int pageIndex, int pageSize)
        {
            return Ok( await mentorRepository.GetAll(pageIndex, pageSize));
        }

        //GET: api/v1/mentors/sorting?pageIndex=1&pageSize=3
        /// <summary>
        /// Get list mentors top rate
        /// </summary>
        [HttpGet("top_rate")]
        public async Task<ActionResult> GetTopByRate(int pageIndex, int pageSize)
        {
            return Ok(await mentorRepository.LoadTopMentorHome(pageIndex, pageSize));
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

        //GET: api/v1/mentors/sorting?pageIndex=1&pageSize=3
        /// <summary>
        /// Sort list mentor by name
        /// </summary>
        [HttpGet("sorting")]
        public async Task<IEnumerable<MentorModel>> SortbyName(string order_by_name, int pageIndex, int pageSize)
        {
            return await mentorRepository.SortByName(order_by_name, pageIndex, pageSize);
        }

        //GET: api/v1/mentors/byName?name=xxx&pageIndex=1&pageSize=3
        /// <summary>
        /// Search mentor by name
        /// </summary>
        [HttpGet("byName")]
        public async Task<ActionResult> FindByName(string name, int pageIndex, int pageSize)
        {
            var mentor = await mentorRepository.FindByName(name, pageIndex, pageSize);
            return Ok(mentor);
        }
        //GET: api/v1/mentors/filter?major=xxx&pageIndex=1&pageSize=3
        /// <summary>
        /// Filter by major name
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult> Filter(string major, int pageIndex, int pageSize)
        {
            var listMentor = await mentorRepository.Filter(major, pageIndex, pageSize);
            return (listMentor.Any()) ? Ok(listMentor) : NotFound();
        }

        //GET: api/v1/mentors/feedbacks/{id}?pageIndex=1&pageSize=3
        /// <summary>
        /// Load comment by mentor id
        /// </summary>
        [HttpGet("feedback/{mentorId}")]
        public async Task<ActionResult> LoadFeedbackMentor(string mentorId, int pageIndex, int pageSize)
        {
            var feedback = await mentorRepository.LoadMentorFeedback(mentorId, pageIndex, pageSize);
            return Ok(feedback);
        }

        [HttpGet("{mentorId}/schedule")]
        public async Task<ActionResult> LoadSchedule(string mentorId, string date)
        {
            return Ok(await mentorRepository.LoadSchedule(mentorId, date));
        }
    }
}
