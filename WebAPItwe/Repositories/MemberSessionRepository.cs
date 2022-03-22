using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Repositories
{
    public class MemberSessionRepository : InMemberSessionRepository
    {
        private readonly dbEWTContext context;

        public MemberSessionRepository(dbEWTContext context)
        {
            this.context = context;
        }

        public async Task<NotificationContentModel> JoinSession(string memberId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>(); 
            var check = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
            if(check == null)
            {
                var member = await context.Members.FindAsync(memberId);
                var memberSession = new MemberSession { 
                    Id = Guid.NewGuid().ToString(),
                    MemberId = memberId,
                    MemberName = member.Fullname,
                    MemberImage = member.Image,
                    MentorVoting = 0,
                    CafeVoting = 0,
                    SessionId = sessionId,
                    Status = false
                };
                context.Add(memberSession);
                await context.SaveChangesAsync();

                // add leader id
                var session = await context.Sessions.FindAsync(sessionId);
                listUserId.Add(session.MemberId);
                notification.image = member.Image;
                notification.content = memberSession.MemberName + " yêu cầu tham gia vào meetup " + session.SubjectName + " của bạn";
                notification.listUserId = listUserId;
            }
            
            return notification;
        }

        public async Task<NotificationContentModel> AcceptMember(string memberId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            var member = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
            var session = await context.Sessions.FindAsync(sessionId);
            if (member != null)
            {
                session.CurrentPerson += 1;
                context.Entry(session).State = EntityState.Modified;
                member.Status = true;
                context.Entry(member).State = EntityState.Modified;
                try
                {
                    await context.SaveChangesAsync();
                    listUserId.Add(memberId);
                    notification.image = session.SubjectImage;
                    notification.listUserId = listUserId;
                    notification.content = "Yêu cầu tham gia vào meetup " + session.SubjectName + " đã được chấp nhận";
                }
                catch
                {
                    throw;
                }            
            }
            return notification;
        }

        public async Task<object> LoadHistory(string memberId, int pageIndex, int pageSize)
        {
            var history = await (from s in context.Sessions 
                                 join m in context.MemberSessions on s.Id equals m.SessionId 
                                 where m.MemberId == memberId && (s.Status == 2 || s.Status == 3) 
                                 select new HistoryModel {
                                        SessionId = s.Id,
                                        SubjectName = s.SubjectName,
                                        Slot = s.Slot,
                                        Date = s.Date,
                                        MentorName = s.MentorName,
                                        Status = s.Status
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return history;
        }

        public async Task<NotificationContentModel> RejectMember(string memberId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            var member = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
            var session = await context.Sessions.FindAsync(sessionId);
            if (member != null)
            {
                context.MemberSessions.Remove(member);
                try
                {
                    await context.SaveChangesAsync();
                    listUserId.Add(memberId);
                    notification.image = session.SubjectImage;
                    notification.listUserId = listUserId;
                    notification.content = "Yêu cầu tham gia vào meetup " + session.SubjectName + " bị từ chối";
                }
                catch
                {
                    throw;
                }
            }
            return notification;
        }

        public async Task<NotificationContentModel> LeaveSession(string memberId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            try
            {
                var session = await context.Sessions.FindAsync(sessionId);
                var member = await context.MemberSessions.Where(x => x.MemberId == memberId)
                    .Where(x => x.SessionId == sessionId).Where(x => x.Status == true).FirstOrDefaultAsync();
                if(member != null)
                {
                    session.CurrentPerson -= 1;
                    context.MemberSessions.Remove(member);
                    await context.SaveChangesAsync();
                    listUserId.Add(session.MentorId);
                    notification.image = member.MemberImage;
                    notification.listUserId = listUserId;
                    notification.content = member.MemberName + " đã rời khỏi meetup " + session.SubjectName;
                }
            }
            catch
            {
                throw;
            }
            return notification;
        }
    }
}
