using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.InRepositories;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly InUserRepository inUserRepository;
        public LoginController(InUserRepository inUserRepository)
        {
            this.inUserRepository = inUserRepository;
        }

        /// <summary>
        /// Login an account
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Login(string userId, string token)
        {
            try
            {
                var role = await inUserRepository.Login(userId, token);
                return Ok(role);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpGet("test")]
        public async Task<ActionResult> Login2(string userId, string token)
        {
            try
            {
                var role = await inUserRepository.Login(userId, token);
                return Ok(role);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
