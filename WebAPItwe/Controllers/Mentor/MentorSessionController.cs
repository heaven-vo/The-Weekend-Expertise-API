using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.InRepositories;

namespace WebAPItwe.Controllers.Mentor
{
    [Route("api/v1/mentor")]
    [ApiController]
    public class MentorSessionController : ControllerBase
    {
        private readonly InSessionRepository sessionRepository;

        public MentorSessionController(InSessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Load the request session(status 0)
        /// </summary>
        [HttpGet("{mentorId}/requests")]
        public async Task<ActionResult> LoadRequestOfMentor(string mentorId, int pageIndex, int pageSize)
        {
            var result = await sessionRepository.LoadRequestOfMentor(mentorId, pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Load the session by status 1(going) or 2(done) or 3 (cancel, reporting)
        /// </summary>
        [HttpGet("{mentorId}/meetups")]
        public async Task<ActionResult> LoadSessionOfMentorByStatus(string mentorId, int status,int pageIndex, int pageSize)
        {
            var result = await sessionRepository.LoadSessionOfMentorByStatus(mentorId, status,pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Load the session in today
        /// </summary>
        [HttpGet("{mentorId}/today_meetup")]
        public async Task<ActionResult> LoadTodayOfMentor(string mentorId)
        {
            var result = await sessionRepository.LoadTodaySessionOfMentor(mentorId);
            return Ok(result);
        }

        /// <summary>
        /// Load the number of request and meet
        /// </summary>
        [HttpGet("{mentorId}/number_home")]
        public async Task<object> LoadNumberSessionMentor(string mentorId)
        {
            return Ok(await sessionRepository.LoadNumberSessionMentor(mentorId));
        }

        /// <summary>
        /// Top skill had booked
        /// </summary>
        [HttpGet("{mentorId}/top_skill")]
        public async Task<object> LoadTopSkill(string mentorId)
        {
            return Ok(await sessionRepository.LoadTopSkill(mentorId));
        }
    }
}
