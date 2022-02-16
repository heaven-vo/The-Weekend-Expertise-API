using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Repositories
{
    public class MemberRepository : InMemberRepository
    {
        private readonly dbEWTContext context;

        public MemberRepository(dbEWTContext context)
        {
            this.context = context;
        }
        public async Task<MemberModel> CreateNewMember(string id, string name)
        {
            var member = new MemberModel { Id = id, Fullname = name };
            context.Add(new Member { Id = id, Fullname = name });
            await context.SaveChangesAsync();
            return member;
        }
    }
}
