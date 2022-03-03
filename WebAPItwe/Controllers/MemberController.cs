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
    [Route("api/v1/members")]
    [ApiController]
    [Produces("application/json")]
    public class MemberController : ControllerBase
    {
        private readonly InMemberRepository inMemberRepository;

        public MemberController( InMemberRepository inMemberRepository)
        {
            this.inMemberRepository = inMemberRepository;
        }
        /// <summary>
        /// Get a member profile
        /// </summary>
        [HttpGet("profile/{username}")]
        public async Task<ActionResult> GetMemberProfile(string username)
        {
            var member = await inMemberRepository.GetMemberProfile(username);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        [HttpPut("profile/{username}")]
        public async Task<ActionResult> UpdateMemberProfile(string username, MemberProfileModel memberProfile)
        {
            try
            {
                var member = await inMemberRepository.UpdateMemberProfile(username, memberProfile);
                return Ok(member);
            }
            catch (Exception e)
            {
                return Conflict();
            }
        }
        [HttpPost("feedback/{memberId}")]
        public async Task<ActionResult> CreateFeedback(string memberId, FeedbackModel feedback)
        {
            try
            {
                var result = await inMemberRepository.CreateFeedback(memberId, feedback);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
