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
        private readonly InSessionRepository sessionRepository;


        public DemandController(InMemberSessionRepository inMemberSessionRepository, InSessionRepository sessionRepository)
        {
            this.inMemberSessionRepository = inMemberSessionRepository;
            this.sessionRepository = sessionRepository;
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
        /// <summary>
        /// Cafe accept request of session
        /// </summary>
        [HttpPut("{sessionId}/cafes/{cafeId}/accept")]
        public async Task<ActionResult> AcceptSessionByCafe(string sessionId, string cafeId)
        {
            try
            {
                await sessionRepository.AcceptSessionByCafe(cafeId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Cafe accept request of session
        /// </summary>
        [HttpPut("{sessionId}/cafes/{cafeId}/cancel")]
        public async Task<ActionResult> CancelSessionByCafe(string sessionId, string cafeId)
        {
            try
            {
                await sessionRepository.CancelSessionByCafe(cafeId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
