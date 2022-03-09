using Microsoft.AspNetCore.Mvc;
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
    public class MentorRepository : InMentorRepository
    {
        private readonly dbEWTContext context;

        public MentorRepository(dbEWTContext context)
        {
            this.context = context;
        }
        //Test query not important
        public IEnumerable<MentorModel> Get()
        {
            var test = from Ads in context.Mentors
                       group Ads by Ads.Sex 
                       into g 
                       select new {g.Key ,Count = g.Count() };
            return (IEnumerable<MentorModel>)context.Mentors.FromSqlRaw("SELECT * FROM Mentor")
                .ToList();
        }
        public async Task<IEnumerable<MentorModel>> GetAll(int pageIndex, int pageSize)
        {
            //var test = await context.Mentors.FromSqlRaw("SELECT * FROM Mentor").ToListAsync();
            var listMentor = await context.Mentors.Select(x => new MentorModel
            { 
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday =x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status

            }).Skip((pageIndex-1)*pageSize).Take(pageSize).ToListAsync();
            foreach(MentorModel mentor in listMentor)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
            return listMentor;
        }

        public async Task<MentorModel> GetById(string id)
        {
            var mentor = await context.Mentors.Where(x => x.Id == id).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday = x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status
            }).FirstOrDefaultAsync();
            if (mentor != null)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
                    
            return mentor;
        }

        public async Task<IEnumerable<MentorModel>> FindByName(string name, int pageIndex, int pageSize)
        {
            var listMentor = await context.Mentors.Where(x => x.Fullname.Contains(name)).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday = x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status
            }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (MentorModel mentor in listMentor)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
            return listMentor;
        }

        public async Task<IEnumerable<MentorModel>> SortByPrice(int pageIndex, int pageSize)
        {
            var listMentor = await context.Mentors.OrderBy(x => x.Price).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday = x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status
            }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (MentorModel mentor in listMentor)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
            return listMentor;
        }

        public async Task<IEnumerable<MentorModel>> Filter(string major, int pageIndex, int pageSize)
        {
            var listMentor = await (from me in context.Mentors 
                              join mt in context.MentorMajors on me.Id equals mt.MentorId 
                              join ma in context.Majors on mt.MajorId equals ma.Id
                              where ma.Name == major 
                              select new MentorModel
                              {
                                  Id = me.Id,
                                  Fullname = me.Fullname,
                                  Address = me.Address,
                                  Slogan = me.Slogan,
                                  Phone = me.Phone,
                                  Image = me.Image,
                                  Sex = me.Sex,
                                  Price = me.Price,
                                  Birthday = me.Birthday,
                                  Rate = me.Rate,
                                  Description = me.Description,
                                  Status = me.Status
                              }
                              ).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (MentorModel mentor in listMentor)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
            return listMentor;
        }
        public async Task<List<string>> GetMajorByMentorId(string mentorId)
        {
            List<string> listMajor = await (from me in context.Mentors
                                join mt in context.MentorMajors on me.Id equals mt.MentorId
                                join ma in context.Majors on mt.MajorId equals ma.Id
                                where me.Id == mentorId
                                select ma.Name                            
                              ).ToListAsync();
            return listMajor;
        }

        public async Task<List<string>> GetSkillByMentorId(string mentorId)
        {
            List<string> listSkill = await (from me in context.Mentors
                                            join mt in context.MentorSkills on me.Id equals mt.MentorId
                                            join s in context.Skills on mt.SkillId equals s.Id
                                            where me.Id == mentorId
                                            select s.Name
                              ).ToListAsync();
            return listSkill;
        }
        public async Task<List<string>> GetCertificateByMentorId(string mentorId)
        {
            List<string> listCertificate = await (from me in context.Mentors
                                            join mt in context.MentorCertificates on me.Id equals mt.MentorId
                                            join s in context.Certificates on mt.CertificateId equals s.Id
                                            where me.Id == mentorId
                                            select s.Name
                              ).Take(3).ToListAsync();
            return listCertificate;
        }

        public async Task<IEnumerable<object>> LoadMentorFeedback(string id, int pageIndex, int pageSize)
        {
            var feedback = await (from me in context.Mentors 
                                           join ses in context.Sessions on me.Id equals ses.MentorId 
                                           join meses in context.MemberSessions on ses.Id equals meses.SessionId 
                                           where me.Id == id && meses.FeedbackOfMentor != null 
                                           select new { meses.MemberName, 
                                                        meses.MemberImage,
                                                        meses.MentorVoting,
                                                        meses.DateMentorFeedback,
                                                        meses.FeedbackOfMentor
                                           }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return feedback; 
        }

        public async Task<object> LoadTopMentorHome(int pageIndex, int pageSize)
        {
            var listMentor = await context.Mentors.OrderByDescending(x => x.Rate).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday = x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status

            }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            foreach (MentorModel mentor in listMentor)
            {
                mentor.Email = await context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
                mentor.ListMajor = await GetMajorByMentorId(mentor.Id);
                mentor.ListSkill = await GetSkillByMentorId(mentor.Id);
                mentor.ListCertificate = await GetCertificateByMentorId(mentor.Id);
            }
            return listMentor;
        }
    }
}
