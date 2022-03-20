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
    public class UserRepository : InUserRepository
    {
        private readonly dbEWTContext context;

        public UserRepository(dbEWTContext context)
        {
            this.context = context;
        }

        public async Task<string> Login(string userId, string token)
        {
            string role = "";
            FcmToken fcm = new FcmToken { Id = Guid.NewGuid().ToString(), UserId = userId, Token = token };
            try
            {
                var user = await context.Users.FindAsync(userId);
                if (user != null)
                {
                    role = user.RoleId;
                }
                var check = await context.FcmTokens.Where(x => x.UserId == userId).Where(x => x.Token == token).FirstOrDefaultAsync();
                if(check != null)
                {
                    context.Add(fcm);
                    await context.SaveChangesAsync();
                }              
                return role;
            }
            catch
            {
                throw;
            }
            
        }

        public async Task Logout(string userId, string token)
        {
            try
            {
                var fcm = await context.FcmTokens.Where(x => x.UserId == userId).Where(x => x.Token == token).FirstOrDefaultAsync();
                context.FcmTokens.Remove(fcm);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RegisterMemberAccount(MemberRegisterModel member)
        {
            var check = context.Users.Where(x => x.Email == member.Email).FirstOrDefault();
            if(check == null)
            {
                context.Add(new User
                {
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    Password = "123456",
                    RoleId = "1",
                    Status = true
                });
                try
                {
                   await context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Email already has used");
            }
        }
    }
}
