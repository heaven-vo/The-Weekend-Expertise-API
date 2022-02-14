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
            return await context.Mentors.Select(x => new MentorModel
            { 
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address

            }).Skip((pageIndex-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<MentorModel> GetById(string id)
        {
            return await context.Mentors.Where(x => x.Id == id).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MentorModel>> FindByName(string name)
        {
            return await context.Mentors.Where(x => x.Fullname.Contains(name)).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address

            }).ToListAsync();
        }

        public async Task<IEnumerable<MentorModel>> SortByPrice()
        {
            return await context.Mentors.OrderBy(x => x.Price).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address

            }).ToListAsync();
        }

        public async Task<IEnumerable<MentorModel>> Filter(string major)
        {
            var listMentor = await (from me in context.Mentors 
                              join mt in context.MentorMajors on me.Id equals mt.MentorId 
                              join ma in context.Majors on mt.MajorId equals ma.Id
                              where ma.Name == major 
                              select new MentorModel
                              {
                                  Id = me.Id,
                                  Fullname = me.Fullname,
                                  Address = me.Address

                              }
                              ).ToListAsync();
            return listMentor;
        }
    }
}
