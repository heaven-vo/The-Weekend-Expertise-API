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
    public class MemberRepository : InMemberRepository
    {
        private readonly dbEWTContext context;

        public MemberRepository(dbEWTContext context)
        {
            this.context = context;
        }
        public async Task<MemberRegisterModel> CreateNewMember(MemberRegisterModel member)
        {
            context.Add(new Member { Id = member.Id, Fullname = member.Name, Birthday = "", Image = "https://firebasestorage.googleapis.com/v0/b/twe-mobile.appspot.com/o/member%2Fistockphoto-1212812078-170667a.jpg?alt=media&token=675b6dba-bb3f-4547-8b76-024e399dec11",
                MajorId = member.MajorId, Status = true });
            await context.SaveChangesAsync();
            return member;
        }

        public async Task<Object> GetMemberProfile(string memberId)
        {
            var member = await (from m in context.Members 
                                join ma in context.Majors on m.MajorId equals ma.Id
                                into Details
                                from defaultVal in Details.DefaultIfEmpty()
                                where m.Id == memberId
                                select new 
                                {
                                    Fullname = m.Fullname,
                                    Image = m.Image,
                                    Address = m.Address,
                                    Phone = m.Phone,
                                    Sex = m.Sex,
                                    Birthday = m.Birthday,
                                    Grade = m.Grade,
                                    MajorName = defaultVal.Name
                                }
                                ).FirstOrDefaultAsync();
            return  member;
        }

        public async Task<object> UpdateMemberProfile(string memberId, MemberProfileModel memberProfile)
        {
            
            if(memberId == null)
            {
                return null; 
            }
            var member = await context.Members.FindAsync(memberId);
            var majorId =  context.Majors.Where(x => x.Name == memberProfile.MajorName).Select(x => x.Id).FirstOrDefault();
            member.Fullname = memberProfile.Fullname;
            member.Image = memberProfile.Image;
            member.Address = memberProfile.Address;
            member.Phone = memberProfile.Phone;
            member.Sex = memberProfile.Sex;
            member.Birthday = memberProfile.Birthday;
            member.MajorId = majorId.ToString();
            member.Grade = memberProfile.Grade;
            context.Entry(member).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }catch
            {
                throw ;
            }
            return memberProfile;
        }
        public async Task<object> CreateFeedback(string memberId, FeedbackModel feedback)
        {
            var session = await context.Sessions.FindAsync(feedback.SessionId);
            if(session.Status == 2) {
                var memberSession = await context.MemberSessions.Where(x => x.MemberId == memberId).Where(x => x.SessionId == feedback.SessionId)
                    .FirstOrDefaultAsync();            
                String dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (memberSession != null)
                {
                    memberSession.MentorVoting = feedback.MentorVoting;
                    memberSession.CafeVoting = feedback.CafeVoting;
                    memberSession.FeedbackOfMentor = feedback.FeedbackOfMentor;
                    memberSession.FeedbackOfCafe = feedback.FeedbackOfCafe;
                    memberSession.DateMentorFeedback = dateCreated;
                    memberSession.DateCafeFeedback = dateCreated;

                    context.Entry(memberSession).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return feedback;
                }
            }
            return null;
        }
    }   
}
