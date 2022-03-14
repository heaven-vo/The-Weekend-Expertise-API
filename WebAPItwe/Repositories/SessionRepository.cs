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
                CafeName = cafeName,
                CafeActive = false
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
                                    MaxPerson = x.MaxPerson,
                                    isJoin = true
                                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (var session in listSessions)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                var cafe = await context.Cafes.FindAsync(cafeId);
                session.CafeDistric = cafe.Distric;
                session.CafeStreet = cafe.Street;
                List<string> listImage = await context.MemberSessions.Where(x => x.SessionId == session.SessionId)
                                        .Where(x => x.Status == true).Select(x => x.MemberImage).Take(5).ToListAsync();
                session.ListMemberImage = listImage;
                session.CurrentPerson = listImage.Count();
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
            var listSessions = await context.Sessions.FromSqlRaw("Select * from Session where MajorId = {0} and Status = 2 and CafeActive = 0 and Session.Id not in (select SessionId from MemberSession where MemberId = {1})", majorId, memberId)
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
                    MaxPerson = x.MaxPerson,
                    isJoin = true
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();


            foreach (var session in listSessions)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                var cafe = await context.Cafes.FindAsync(cafeId);
                session.CafeDistric = cafe.Distric;
                session.CafeStreet = cafe.Street;
                List<string> listImage = await context.MemberSessions.Where(x => x.SessionId == session.SessionId)
                                        .Where(x => x.Status == true).Select(x => x.MemberImage).Take(5).ToListAsync();
                session.ListMemberImage = listImage;
                session.CurrentPerson = listImage.Count();
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
            sessionDetail.ListMentor = await getListMentor(sessionId);
            sessionDetail.ListMember = await getListMember(sessionId, true);
            //Confirm who what detail is leader
            var leadId = await context.Sessions.Where(x => x.Id == sessionId).Select(x => x.MemberId).FirstOrDefaultAsync();
            if(leadId == memberId)
            {
                sessionDetail.isLead = true;
            }
            return sessionDetail;
        }

        public async Task<object> LoadRequestMember(string sessionId)
        {
            return await getListMember(sessionId, false);
        }

        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
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
            List<MentorInSessionModel> list = new List<MentorInSessionModel>();
            var listMentorId = await context.MentorSessions.Where(x => x.SessionId == sessionId).Select(x => x.MentorId).ToListAsync();
            
            foreach (var mentorId in listMentorId)
            {
                var mentor = await context.Mentors.Where(x => x.Id == mentorId)
                    .Select(x => new MentorInSessionModel
                    {
                        Id = x.Id,
                        Name= x.Fullname,
                        Image = x.Image,
                        Rate = x.Rate
                    }).FirstOrDefaultAsync();
                list.Add(mentor);
            }
            return list;
        }
        public async Task<List<MemberInSessionModel>> getListMember(string sessionId, bool status)
        {
            var listMember = await context.MemberSessions.Where(x => x.SessionId == sessionId).Where(x => x.Status == status)
                .Select(x => new MemberInSessionModel 
                {
                    Id =x.MemberId,
                    Name = x.MemberName,
                    Image = x.MemberImage
                   
                }).ToListAsync();
            foreach(var member in listMember)
            {
                var majorName = await (from mem in context.Members
                                       join ma in context .Majors on mem.MajorId equals ma.Id 
                                       where mem.Id == member.Id 
                                       select ma.Name).FirstOrDefaultAsync();
                member.MajorName = majorName;
            }
            return listMember;
        }

        public async Task AcceptSessionByCafe(string cafeId, string sessionId)
        {
            var session = await context.Sessions.Where(x => x.CafeId == cafeId).Where(x => x.Id == sessionId).FirstOrDefaultAsync();
            if (session != null)
            {
                session.Status = 1;
                context.Entry(session).State = EntityState.Modified;
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

        public async Task CancelSessionByCafe(string cafeId, string sessionId)
        {
            var session = await context.Sessions.Where(x => x.CafeId == cafeId).Where(x => x.Id == sessionId).FirstOrDefaultAsync();
            if (session != null)
            {
                session.Status = 4;
                context.Entry(session).State = EntityState.Modified;
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
    }
}
