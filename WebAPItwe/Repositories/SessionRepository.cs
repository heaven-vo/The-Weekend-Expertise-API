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
            Session session = new Session
            {
                Id = sessionId,
                Slot = newSession.Slot,
                Date = newSession.Date,
                DateCreated = dateCreated,
                MaxPerson = newSession.MaxPerson,
                Status = 0,
                MemberId = newSession.MemberId,
                MajorId = newSession.MajorId,
                SubjectId = newSession.SubjectId,
                SubjectName = subject.Name,
                SubjectImage = subject.Image,
                CafeId = newSession.CafeId,
                CafeName = cafeName
            };
            context.Add(session);
            var memberSession = new MemberSession
            {
                Id = Guid.NewGuid().ToString(),
                MemberId = newSession.MemberId,
                MemberName = newSession.MemberName,
                MemberImage = newSession.MemberImage,
                MentorVoting = 0,
                CafeVoting = 0,
                SessionId = sessionId,
                Status = true
            };
            context.Add(memberSession);
            foreach (var mentorId in newSession.ListMentor)
            {
                var mentor = new MentorSession { Id = Guid.NewGuid().ToString(), MentorId = mentorId, SessionId = sessionId, RequestDate = dateCreated, Status = false };
                context.Add(mentor);
            }
            //Bo sua lai
            var payment = new Payment { Id = Guid.NewGuid().ToString(), Amount = newSession.Payments.Amount, Type = newSession.Payments.Type, SessionId = sessionId, Status = "true" };
            context.Add(payment);
            await context.SaveChangesAsync();
        }        

        public async Task<object> LoadSession(string memberId, int pageIndex, int pageSize)
        {
            var listSessions = await context.Sessions.Where(x => x.Status == 2).Where(x => x.CafeActive == false)
                                .Select(x => new SessionHomeModel
                                {
                                    SessionId = x.Id,
                                    SubjectImage = x.SubjectImage,
                                    SubjectName = x.SubjectName,
                                    Date = x.Date,
                                    Slot = x.Slot,
                                    CafeName = x.CafeName,
                                    MentorName = x.MentorName,
                                    Price = x.Price,
                                    isJoin = true
                                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (var session in listSessions)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                var cafe = await context.Cafes.FindAsync(cafeId);
                session.CafeDistric = cafe.Distric;
                session.CafeStreet = cafe.Street;
                List<string> listImage = await context.MemberSessions.Where(x => x.SessionId == session.SessionId).Select(x => x.MemberImage).Take(5).ToListAsync();
                session.ListMemberImage = listImage;
                var join = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == session.SessionId).FirstOrDefaultAsync();
                if (join == null)
                {
                    session.isJoin = false;
                }
            }

            return listSessions;
        }
        public async Task<object> LoadRecommendSession(string memberId, int pageIndex, int pageSize)
        {
            string majorId = await context.Members.Where(x => x.Id == memberId).Select(x => x.MajorId).FirstOrDefaultAsync();
            var listSessions = await context.Sessions.FromSqlRaw("Select * from Session where Status = 2 and CafeActive = 0 and Session.Id not in (select SessionId from MemberSession where MemberId = {0})", memberId)
                .Select(x => new SessionHomeModel
                {
                    SessionId = x.Id,
                    SubjectImage = x.SubjectImage,
                    SubjectName = x.SubjectName,
                    Date = x.Date,
                    Slot = x.Slot,
                    CafeName = x.CafeName,
                    MentorName = x.MentorName,
                    Price = x.Price,
                    isJoin = true
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


            foreach (var session in listSessions)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                var cafe = await context.Cafes.FindAsync(cafeId);
                session.CafeDistric = cafe.Distric;
                session.CafeStreet = cafe.Street;
                List<string> listImage = await context.MemberSessions.Where(x => x.SessionId == session.SessionId).Select(x => x.MemberImage).Take(5).ToListAsync();
                session.ListMemberImage = listImage;
                var join = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == session.SessionId).FirstOrDefaultAsync();
                if (join == null)
                {
                    session.isJoin = false;
                }
            }

            return listSessions;
        }

        public async Task<object> LoadSessionDetail(string memberId, string sessionId)
        {
            var sessionDetail = await context.Sessions.Where(x => x.Id == sessionId)
                .Select(x => new SessionDetailModel {
                    SessionId = x.Id,
                    MajorId = x.MajorId,
                    SubjectName = x.SubjectName,
                    SubjectImage = x.SubjectImage,
                    Price = x.Price,
                    Date = x.Date,
                    Slot = x.Slot,
                    MaxPerson = x.MaxPerson,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            sessionDetail.MajorName = await context.Majors.Where(x => x.Id == sessionDetail.MajorId).Select(x => x.Name).FirstOrDefaultAsync();
            sessionDetail.Cafe = await getCafeBySessionId(sessionId);

            //Confirm who what detail is leader
            var leadId = await context.Sessions.Where(x => x.Id == sessionId).Select(x => x.MemberId).FirstOrDefaultAsync();
            if(leadId == memberId)
            {
                sessionDetail.isLead = true;
            }
            return sessionDetail;
        }
        public async Task<CafeModel> getCafeBySessionId(string sessionId)
        {
            var cafeId = await context.Sessions.Where(x => x.Id == sessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
            var cafe = await context.Cafes.Where(x => x.Id == cafeId)
                .Select(x => new CafeModel 
                {
                 Id = x.Id,
                 Name = x.Name,
                 Image = x.Image,
                 OpenTime = x.OpenTime,
                 CloseTime = x.CloseTime,
                 Street = x.Street,
                 Distric = x.Distric,
                 Description = x.Description,
                 Price = x.Price,
                 Rate = x.Rate
                }).FirstOrDefaultAsync();
            return cafe;
        }
        public async Task<List<MentorInSessionModel>> getListMentor(string sessionId)
        {
            var listMentorId = await context.MentorSessions.Where(x => x.Id == sessionId).Select(x => x.MentorId).ToListAsync();
            return null;
        }
        public async Task<List<MemberInSessionModel>> getListMember(string sessionId)
        {
            var listMemberId = await context.MemberSessions.Where(x => x.Id == sessionId).Select(x => x.MemberId).ToListAsync();
            return null;
        }
    }
}
