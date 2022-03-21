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

            var skill = await context.Skills.FindAsync(newSession.SkillId);
            string cafeName = await context.Cafes.Where(x => x.Id == newSession.CafeId).Select(x => x.Name).FirstOrDefaultAsync();
            var member = await context.Members.FindAsync(newSession.MemberId);
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
                SubjectId = newSession.SkillId,
                SubjectName = skill.Name,
                SubjectImage = skill.Image,
                CafeId = newSession.CafeId,
                CafeName = cafeName,
                CafeActive = false,
                CurrentPerson = 1
            };
            context.Add(session);
            var memberSession = new MemberSession
            {
                Id = Guid.NewGuid().ToString(),
                MemberId = newSession.MemberId,
                MemberName = member.Fullname,
                MemberImage = member.Image,
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
            var listSessions = await context.Sessions.Where(x => x.Status == 1).Where(x => x.CafeActive == false).Where(x => x.CurrentPerson < 5)
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
                                    isJoin = 0
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
                if (join != null)
                {
                    session.isJoin = 1;
                    if (join.Status == true)
                    {
                        session.isJoin = 2;
                    }
                }
            }

            return listSessions;
        }
        public async Task<object> LoadSessionByMajor(string memberId, string majorId, int pageIndex, int pageSize)
        {
            var listSessions = await context.Sessions.Where(x => x.CurrentPerson < 5).Where(x => x.Status == 1).Where(x => x.MajorId == majorId).Where(x => x.CafeActive == false)
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
                                    isJoin = 0
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
                if (join != null)
                {
                    session.isJoin = 1;
                    if (join.Status == true)
                    {
                        session.isJoin = 2;
                    }
                }
            }

            return listSessions;
        }
        public async Task<object> LoadRecommendSession(string memberId, int pageIndex, int pageSize)
        {
            string majorId = await context.Members.Where(x => x.Id == memberId).Select(x => x.MajorId).FirstOrDefaultAsync();
            var listSessions = await context.Sessions.FromSqlRaw("Select * from Session where CurrentPerson < 5 MajorId = {0} and Status = 1 and CafeActive = 0 and Session.Id not in (select SessionId from MemberSession where MemberId = {1})", majorId, memberId)
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
                    isJoin = 0
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
                if (join != null)
                {
                    session.isJoin = 1;
                }
            }

            return listSessions;
        }
        public async Task<object> LoadMySession(string memberId, int pageIndex, int pageSize)
        {
            var listSession = await (from se in context.Sessions
                                     join ms in context.MemberSessions on se.Id equals ms.SessionId
                                     where ms.MemberId == memberId
                                     select new MySessionModel
                                     {
                                         Id = se.Id,
                                         SubjectImage = se.SubjectImage,
                                         SubjectName = se.SubjectName,
                                         Slot = se.Slot,
                                         Date = se.Date,
                                         Status = se.Status,
                                         isLead = false
                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (var session in listSession)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.Id).Select(x => x.CafeId).FirstOrDefaultAsync();
                session.CafeName = await context.Cafes.Where(x => x.Id == cafeId).Select(x => x.Name).FirstOrDefaultAsync();
                session.ListMentor = await getListMentor(session.Id);

                var leadId = await context.Sessions.Where(x => x.Id == session.Id).Select(x => x.MemberId).FirstOrDefaultAsync();
                if (leadId == memberId)
                {
                    session.isLead = true;
                }
            }
            return listSession;
        }

        public async Task<object> LoadMySessionByStatus(string memberId, int status, int pageIndex, int pageSize)
        {
            var listSession = await (from se in context.Sessions
                                     join ms in context.MemberSessions on se.Id equals ms.SessionId
                                     where ms.MemberId == memberId && se.Status == status
                                     select new MySessionModel
                                     {
                                         Id = se.Id,
                                         SubjectImage = se.SubjectImage,
                                         SubjectName = se.SubjectName,
                                         Slot = se.Slot,
                                         Date = se.Date,
                                         Status = se.Status,
                                         isLead = false
                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (var session in listSession)
            {
                var cafeId = await context.Sessions.Where(x => x.Id == session.Id).Select(x => x.CafeId).FirstOrDefaultAsync();
                session.CafeName = await context.Cafes.Where(x => x.Id == cafeId).Select(x => x.Name).FirstOrDefaultAsync();
                session.ListMentor = await getListMentor(session.Id);

                var leadId = await context.Sessions.Where(x => x.Id == session.Id).Select(x => x.MemberId).FirstOrDefaultAsync();
                if (leadId == memberId)
                {
                    session.isLead = true;
                }
            }
            return listSession;
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
                    Status = x.Status,
                    isJoin = 0,
                    isLead = false,
                    isFeed = false
                }).FirstOrDefaultAsync();
            sessionDetail.MajorName = await context.Majors.Where(x => x.Id == sessionDetail.MajorId).Select(x => x.Name).FirstOrDefaultAsync();
            sessionDetail.Cafe = await getCafeBySessionId(sessionId);
            sessionDetail.ListMentor = await getListMentor(sessionId);
            sessionDetail.ListMember = await getListMember(sessionId, true);
            //Confirm is join session
            var join = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == sessionDetail.SessionId).FirstOrDefaultAsync();
            if (join != null)
            {
                sessionDetail.isJoin = 1;
                if (join.Status == true)
                {
                    sessionDetail.isJoin = 2;
                    if (join.MentorVoting != 0 || join.FeedbackOfMentor != null)
                    {
                        sessionDetail.isFeed = true;
                    }
                }
            }
            //Confirm who what detail is leader
            var leadId = await context.Sessions.Where(x => x.Id == sessionId).Select(x => x.MemberId).FirstOrDefaultAsync();
            if (leadId == memberId)
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
            var session = await context.Sessions.FindAsync(sessionId);
            if (session.Status == 1 || session.Status == 2)
            {
                var mentor = await context.Mentors.Where(x => x.Id == session.MentorId)
                    .Select(x => new MentorInSessionModel
                    {
                        Id = x.Id,
                        Name = x.Fullname,
                        Image = x.Image,
                        Rate = x.Rate
                    }).FirstOrDefaultAsync();
                list.Add(mentor);
            }
            else
            {
                var listMentorId = await context.MentorSessions.Where(x => x.SessionId == sessionId).Select(x => x.MentorId).ToListAsync();

                foreach (var mentorId in listMentorId)
                {
                    var mentor = await context.Mentors.Where(x => x.Id == mentorId)
                        .Select(x => new MentorInSessionModel
                        {
                            Id = x.Id,
                            Name = x.Fullname,
                            Image = x.Image,
                            Rate = x.Rate
                        }).FirstOrDefaultAsync();
                    list.Add(mentor);
                }
            }

            return list;
        }
        public async Task<List<MemberInSessionModel>> getListMember(string sessionId, bool status)
        {
            var listMember = await context.MemberSessions.Where(x => x.SessionId == sessionId).Where(x => x.Status == status)
                .Select(x => new MemberInSessionModel
                {
                    Id = x.MemberId,
                    Name = x.MemberName,
                    Image = x.MemberImage

                }).ToListAsync();
            foreach (var member in listMember)
            {
                var majorName = await (from mem in context.Members
                                       join ma in context.Majors on mem.MajorId equals ma.Id
                                       where mem.Id == member.Id
                                       select ma.Name).FirstOrDefaultAsync();
                member.MajorName = majorName;
            }
            return listMember;
        }

        //------------------------------------------------------------------------------------------------------------
        public async Task<NotificationContentModel> AcceptSessionByMentor(string mentorId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            var mentor = await context.Mentors.FindAsync(mentorId);
            var session = await context.Sessions.Where(x => x.Id == sessionId).FirstOrDefaultAsync();
            if (session != null && session.MentorId == null)
            {
                session.MentorId = mentorId;
                session.MentorName = mentor.Fullname;
                session.Price = mentor.Price;
                session.Status = 1;
                context.Entry(session).State = EntityState.Modified;

                var mentorSession = await context.MentorSessions.Where(x => x.MentorId == mentorId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
                mentorSession.Status = true;
                context.Entry(mentorSession).State = EntityState.Modified;
                try
                {
                    await context.SaveChangesAsync();
                    listUserId.Add(session.MemberId);
                    notification.listUserId = listUserId;
                    notification.content = "Lời mời tham dự meetup " + session.SubjectName + " đã được mentor " + session.MentorName + " chấp nhận";
                    return notification;
                }
                catch
                {
                    throw;
                }
            }
            else throw new Exception("Conflict");
        }

        public async Task<NotificationContentModel> RejectSessionByMentor(string mentorId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            var session = await context.Sessions.FindAsync(sessionId);
            var ms = await context.MentorSessions.Where(x => x.MentorId == mentorId).Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
            if (ms != null && ms.Status == false)
            {
                context.MentorSessions.Remove(ms);
                try
                {
                    await context.SaveChangesAsync();
                    listUserId.Add(session.MemberId);
                    notification.listUserId = listUserId;
                    notification.content = "Lời mời tham dự meetup " + session.SubjectName + " đã bị mentor " + session.MentorName + " từ chối";
                    return notification;
                }
                catch
                {
                    throw;
                }
            }
            else throw new Exception("Conflict");
        }

        public async Task<NotificationContentModel> CancelSession(string userId, string sessionId)
        {
            NotificationContentModel notification = new NotificationContentModel();
            List<string> listUserId = new List<string>();
            var member = await context.Members.FindAsync(userId);
            var session = await context.Sessions.FindAsync(sessionId);
            session.Status = 3;
            context.Entry(session).State = EntityState.Modified;
            try
            {
                if (member != null)
                {
                    var listMemberId = await context.MemberSessions.Where(x => x.SessionId == sessionId).Where(x => x.Status == true).Select(x => x.MemberId).ToListAsync();
                    listUserId = listMemberId;
                    listUserId.Add(session.MentorId);
                    listUserId.Remove(userId);
                    notification.content = "Meetup " + session.SubjectName + " đã bị hủy bởi " + member.Fullname;
                }
                else
                {
                    var listMemberId = await context.MemberSessions.Where(x => x.SessionId == sessionId).Where(x => x.Status == true).Select(x => x.MemberId).ToListAsync();
                    listUserId = listMemberId;
                    notification.content = "Meetup " + session.SubjectName + " đã bị hủy bởi mentor " + session.MentorName;
                }
                await context.SaveChangesAsync();
                notification.listUserId = listUserId;

            }
            catch
            {
                throw;
            }

            return notification;
        }

        // Mentor Session --------------------------------------------------------------------------------
        public async Task<object> LoadRequestOfMentor(string mentorId, int pageIndex, int pageSize)
        {
            var listSession = await (from s in context.Sessions
                                     join ms in context.MentorSessions on s.Id equals ms.SessionId
                                     join m in context.Members on s.MemberId equals m.Id
                                     where ms.MentorId == mentorId && s.Status == 0
                                     select new
                                     {
                                         s.Id,
                                         s.SubjectImage,
                                         s.SubjectName,
                                         s.Slot,
                                         s.Date,
                                         memberName = m.Fullname,
                                         s.CafeName

                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return listSession;
        }

        class MentorSessionModel
        {
            public string Id { get; set; }
            public string? SessionName { get; set; }
            public string SessionImage { get; set; }
            public int Slot { get; set; }
            public string Date { get; set; }
            public string CafeName { get; set; }
            public int Status { get; set; }
            public List<string> ListImage { get; set; }
        }

        public async Task<object> LoadSessionOfMentorByStatus(string mentorId, int status, int pageIndex, int pageSize)
        {
            var listSession = new List<MentorSessionModel>();
            if (status == 1 || status == 2)
            {
                listSession = await (from s in context.Sessions
                                     join m in context.Members on s.MemberId equals m.Id
                                     where s.MentorId == mentorId && s.Status == status
                                     select new MentorSessionModel
                                     {
                                         Id = s.Id,
                                         SessionImage = s.SubjectImage,
                                         SessionName = s.SubjectName,
                                         Slot = s.Slot,
                                         Date = s.Date,
                                         CafeName = s.CafeName,
                                         Status = s.Status
                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var session in listSession)
                {
                    var listImage = await context.MemberSessions.Where(x => x.SessionId == session.Id).Where(x => x.Status == true)
                        .Select(x => x.MemberImage).ToListAsync();
                    session.ListImage = listImage;
                }
            }
            if (status == 3)
            {
                listSession = await (from s in context.Sessions
                                     join m in context.Members on s.MemberId equals m.Id
                                     where s.MentorId == mentorId && (s.Status == 3 || s.Status == 4)
                                     select new MentorSessionModel
                                     {
                                         Id = s.Id,
                                         SessionImage = s.SubjectImage,
                                         SessionName = s.SubjectName,
                                         Slot = s.Slot,
                                         Date = s.Date,
                                         CafeName = s.CafeName,
                                         Status = s.Status
                                     }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var session in listSession)
                {
                    var listImage = await context.MemberSessions.Where(x => x.SessionId == session.Id).Where(x => x.Status == true)
                        .Select(x => x.MemberImage).ToListAsync();
                    session.ListImage = listImage;
                }
            }
            return listSession;
        }
        class number
        {
            public int request { get; set; }
            public int meetup { get; set; }
        }
        public async Task<object> LoadNumberSessionMentor(string mentorId)
        {
            var request = await (from s in context.Sessions
                                     join ms in context.MentorSessions on s.Id equals ms.SessionId
                                     where ms.MentorId == mentorId && s.Status == 0
                                     select new Session()).ToListAsync();
            var meetup = await context.Sessions.Where(x => x.MentorId == mentorId).Where(x => x.Status == 1).ToListAsync();
            var number = new number{ request = request.Count(), meetup = meetup.Count() };
            return number;
        }
    }
}
