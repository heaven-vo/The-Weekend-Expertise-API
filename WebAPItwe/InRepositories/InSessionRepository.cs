using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InSessionRepository
    {
        Task CreateNewSession(NewSessionModel newSession);
        Task<object> LoadSession(string memberId, int pageIndex, int pageSize);
        Task<object> LoadRecommendSession(string memberId, int pageIndex, int pageSize);
        Task<object> LoadMySession(string memberId, int pageIndex, int pageSize);
        Task<object> LoadSessionByMajor(string memberId, string majorId, int pageIndex, int pageSize);
        Task<object> LoadMySessionByStatus(string memberId, int status, int pageIndex, int pageSize);
        Task<object> LoadSessionDetail(string memberId, string sessionId);
        Task<object> LoadRequestMember(string sessionId);
        Task<NotificationContentModel> AcceptSessionByMentor(string mentorId, string sessionId);
        Task<NotificationContentModel> RejectSessionByMentor(string mentorId, string sessionId);
        Task<NotificationContentModel> CancelSession(string userId, string sessionId);
        Task<object> LoadRequestOfMentor(string mentorId, int pageIndex, int pageSize);
        Task<object> LoadSessionOfMentorByStatus(string mentorId, int status, int pageIndex, int pageSize);
        Task<object> LoadNumberSessionMentor(string mentorId);
    }
}
