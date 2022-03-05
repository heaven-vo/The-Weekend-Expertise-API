using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.InRepositories
{
    public interface InMemberSessionRepository
    {
        Task<Object> LoadHistory(string memberId, int pageIndex, int pageSize);
    }
}
