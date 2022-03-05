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
    public class SessionRepository : InSessionRepository 
    {
        private readonly dbEWTContext context;

        public SessionRepository(dbEWTContext context)
        {
            this.context = context;
        }

        public async Task CreateNewSession(NewSessionModel newSession)
        {
            string sessionId = Guid.NewGuid().ToString();
            string dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var subject = await context.Subjects.FindAsync(newSession.SubjectId);
            string cafeName = await context.Cafes.Where(x => x.Id == newSession.CafeId).Select(x => x.Name).FirstOrDefaultAsync();
            Session session = new Session { Id = sessionId, Slot = newSession.Slot, Date = newSession.Date,
                                            DateCreated = dateCreated, MaxPerson = newSession.MaxPerson, Status = "WaitCafe",
                                            MemberId = newSession.MemberId, MajorId = newSession.MajorId, SubjectId = newSession.SubjectId, 
                                            SubjectName = subject.Name, SubjectImage = subject.Image, CafeId = newSession.CafeId, CafeName = cafeName};
            context.Add(session);
            var memberSession = new MemberSession {Id = Guid.NewGuid().ToString(), MemberId = newSession.MemberId, MemberName = newSession.MemberName,
                                                   MemberImage = newSession.MemberImage ,MentorVoting = 0, CafeVoting = 0, SessionId = sessionId, Status = true};
            context.Add(memberSession);
            foreach(var mentorId in newSession.ListMentor)
            {
                var mentor = new MentorSession {Id = Guid.NewGuid().ToString(), MentorId = mentorId, SessionId = sessionId, RequestDate = dateCreated, Status = false };
                context.Add(mentor);
            }
            //Bo sua lai
            var payment = new Payment { Id = Guid.NewGuid().ToString(), Amount = newSession.Payments.Amount, Type = newSession.Payments.Type, SessionId = sessionId ,Status = "true" };
            context.Add(payment);
            await context.SaveChangesAsync();
        }

        public async Task<object> LoadRecommendSession(string memberId)
        {
            string majorId = await context.Members.Where(x => x.Id == memberId).Select(x => x.MajorId).FirstOrDefaultAsync();
            var listSessions = await context.Sessions.Where(x => x.MajorId == majorId).Where(x => x.Status == "Going")
                                .Select(x => new SessionHomeModel 
                                {
                                    SessionId = x.Id,
                                    SubjectImage = x.SubjectImage, 
                                    SubjectName = x.SubjectName,
                                    Date = x.Date,
                                    Slot = x.Slot,
                                    CafeName = x.CafeName,
                                    MentorName = x.MentorName,
                                    Price = x.Price
                                }).Take(4).ToListAsync();
            foreach(var session in listSessions)
            {
                List<string> listImage = await context.MemberSessions.Where(x => x.SessionId == session.SessionId).Select(x => x.MemberImage).Take(5).ToListAsync();
                session.ListMemberImage = listImage;
            }
            
            return listSessions;
        }
    }
}
