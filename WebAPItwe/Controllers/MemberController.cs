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
        [HttpGet("profile/{userId}")]
        public async Task<ActionResult> GetMemberProfile(string userId)
        {
            var member = await inMemberRepository.GetMemberProfile(userId);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        [HttpPut("profile/{userId}")]
        public async Task<ActionResult> UpdateMemberProfile(string userId, MemberProfileModel memberProfile)
        {
            try
            {
                var member = await inMemberRepository.UpdateMemberProfile(userId, memberProfile);
                return Ok(member);
            }
            catch
            {
                return Conflict();
            }
        }
        [HttpPost("new_feedback/{memberId}")]
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
