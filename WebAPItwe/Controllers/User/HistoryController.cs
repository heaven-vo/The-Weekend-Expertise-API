using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebAPItwe.InRepositories;

namespace WebAPItwe.Controllers.User
{
    [Route("api/v1/histories")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly InMemberSessionRepository inMemberSessionRepository;

        public HistoryController(InMemberSessionRepository inMemberSessionRepository)
        {
            this.inMemberSessionRepository = inMemberSessionRepository;
        }
        [HttpGet("{memberId}")]
        public async Task<ActionResult> LoadHistory(string memberId, int pageIndex, int pageSize)
        {
            var history = await inMemberSessionRepository.LoadHistory(memberId, pageIndex, pageSize);
            return Ok(history);
        }
    }
}
