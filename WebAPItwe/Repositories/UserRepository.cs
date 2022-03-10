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
