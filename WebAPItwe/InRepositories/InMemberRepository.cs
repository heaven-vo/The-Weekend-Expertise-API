using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InMemberRepository
    {
        Task<MemberModel> CreateNewMember(string id, string name);
    }
}
