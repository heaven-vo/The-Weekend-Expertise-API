﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<MemberModel> CreateNewMember(string id, string name)
        {
            var member = new MemberModel { Id = id, Fullname = name };
            context.Add(new Member { Id = id, Fullname = name, Birthday = "", Status = true });
            await context.SaveChangesAsync();
            return member;
        }

        public async Task<Object> GetMemberProfile(string username)
        {
            string memberId =  context.Users.Where(x => x.Email == username).Select(x => x.Id).FirstOrDefault();
            Console.WriteLine(memberId);
            //if (memberId == null) return null;
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

        public async Task<object> UpdateMemberProfile(string username, MemberProfileModel memberProfile)
        {
            
            string memberId = context.Users.Where(x => x.Email == username).Select(x => x.Id).FirstOrDefault();
            if(memberId == null)
            {
                Console.WriteLine("memberId null");
                return null; 
            }
            Console.WriteLine("memberId not null");
            var member = await context.Members.FindAsync(memberId);
            var majorId =  context.Majors.Where(x => x.Name == memberProfile.MajorName).Select(x => x.Id).FirstOrDefault();
            Console.WriteLine("MajorID: " + majorId.ToString());
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
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw ;
            }
            return memberProfile;
        }
    }
}
