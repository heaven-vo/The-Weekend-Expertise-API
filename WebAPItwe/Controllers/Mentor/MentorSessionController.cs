﻿using Microsoft.AspNetCore.Http;
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
        /// Load the session by status 1(going) or 2(done)
        /// </summary>
        [HttpGet("{mentorId}/meetups")]
        public async Task<ActionResult> LoadSessionOfMentorByStatus(string mentorId, int status,int pageIndex, int pageSize)
        {
            var result = await sessionRepository.LoadSessionOfMentorByStatus(mentorId, status,pageIndex, pageSize);
            return Ok(result);
        }
    }
}