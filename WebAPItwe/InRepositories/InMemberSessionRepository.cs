using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InMemberSessionRepository
    {
        Task<Object> LoadHistory(string memberId, int pageIndex, int pageSize);
        Task<NotificationContentModel> JoinSession(string memberId, string sessionId);
        Task<NotificationContentModel> AcceptMember(string memberId, string sessionId);
        Task<NotificationContentModel> RejectMember(string memberId, string sessionId);
    }
}
