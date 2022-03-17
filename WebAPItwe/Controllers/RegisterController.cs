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
    [Route("api/v1/newAccounts")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly InUserRepository inUserRepository;
        private readonly InMemberRepository inMemberRepository;

        public RegisterController(InUserRepository inUserRepository, InMemberRepository inMemberRepository)
        {
            this.inUserRepository = inUserRepository;
            this.inMemberRepository = inMemberRepository;
        }
        /// <summary>
        /// Register a member account and a member
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> RegisterMember(MemberRegisterModel member)
        {
            MemberRegisterModel newMember = null;
            try
            {
                await inUserRepository.RegisterMemberAccount(member);
                newMember = await inMemberRepository.CreateNewMember(member);
            }
            catch
            {
                return Conflict();
            }
            return Ok(newMember);
        }
    }
}
