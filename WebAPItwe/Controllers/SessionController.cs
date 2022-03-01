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

    }
}
