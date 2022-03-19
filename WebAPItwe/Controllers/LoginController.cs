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
        /// Login an account and save fcm token to db
        /// </summary>
        [HttpPost]
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

        /// <summary>
        /// Log out an account and remove fcm token
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Logout(string userId, string token)
        {
            try
            {
                await inUserRepository.Logout(userId, token);
                return NoContent();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
