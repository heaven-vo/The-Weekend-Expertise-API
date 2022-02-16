using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InUserRepository
    {
        Task<string> RegisterMemberAccount(MemberRegisterModel member);
    }
}
