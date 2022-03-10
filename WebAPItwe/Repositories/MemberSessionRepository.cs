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

        public async Task JoinSession(string memberId, string sessionId)
        {
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
            }
        }

        public async Task AcceptMember(string memberId, string sessionId)
        {
            var member = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
            if (member != null)
            {
                member.Status = true;
                context.Entry(member).State = EntityState.Modified;
                try
                {
                    await context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<object> LoadHistory(string memberId, int pageIndex, int pageSize)
        {
            var history = await (from s in context.Sessions 
                                 join m in context.MemberSessions on s.Id equals m.SessionId 
                                 where m.MemberId == memberId && (s.Status == 3 || s.Status ==4) 
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
    }
}
