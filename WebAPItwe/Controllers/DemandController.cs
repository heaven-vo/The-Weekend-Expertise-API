using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.InRepositories;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/session-management")]
    [ApiController]
    public class DemandController : ControllerBase
    {
        private readonly InMemberSessionRepository inMemberSessionRepository;

        public DemandController(InMemberSessionRepository inMemberSessionRepository)
        {
            this.inMemberSessionRepository = inMemberSessionRepository;
        }
        /// <summary>
        /// Member push a request to join existed session
        /// </summary>
        [HttpPost("{sessionId}/members/{memberId}/join")]
        public async Task<ActionResult> JoinSession (string sessionId, string memberId)
        {
            try
            {
                await inMemberSessionRepository.JoinSession(memberId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }           
        }
        /// <summary>
        /// Leader of session accept request of memberId
        /// </summary>
        [HttpPut("{sessionId}/members/{memberId}/accept")]
        public async Task<ActionResult> AcceptMember(string sessionId, string memberId)
        {
            try
            {
                await inMemberSessionRepository.AcceptMember(memberId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
