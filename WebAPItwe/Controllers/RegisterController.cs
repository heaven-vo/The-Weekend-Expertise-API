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
        [HttpPost]
        public async Task<ActionResult<MemberModel>> RegisterMember(MemberRegisterModel member)
        {
            MemberModel newMember = null;
            try
            {
                string id = await inUserRepository.RegisterMemberAccount(member);
                newMember = await inMemberRepository.CreateNewMember(id, member.Name);
            }
            catch
            {
                return Conflict();
            }
            //return CreatedAtAction("NewMember", new { id = newMember.Id }, newMember);
            //return Ok();
            return Ok(newMember);
        }
    }
}
