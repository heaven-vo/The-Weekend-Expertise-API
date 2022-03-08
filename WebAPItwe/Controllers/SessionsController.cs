using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly InSessionRepository sessionRepository;

        public SessionController(InSessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }
        /// <summary>
        /// Create new Session 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateNewSession(NewSessionModel newSession)
        {
            try
            {
                await sessionRepository.CreateNewSession(newSession);
                return Ok();
            }
            catch
            {
                return Conflict();
            }

        }
        /// <summary>
        /// Load recommend session by major of member (for home page)  
        /// </summary>
        [HttpGet("home")]
        public async Task<ActionResult> LoadRecommendSession(string memberId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadRecommendSession(memberId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load all session (for search page)  
        /// </summary>
        [HttpGet()]
        public async Task<ActionResult> LoadSession(string memberId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadSession(memberId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }
        /// <summary>
        /// Load detail of session (for search page)  
        /// </summary>
        [HttpGet("detail")]
        public async Task<ActionResult> LoadSessionDetail(string memberId, string sessionId)
        {
            try
            {
                var session = await sessionRepository.LoadSessionDetail(memberId, sessionId);
                return Ok(session);
            }
            catch
            {
                return Conflict();
            }

        }
    }
}
