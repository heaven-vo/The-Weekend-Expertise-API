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

        public async Task<string> RegisterMemberAccount(MemberRegisterModel member)
        {
            var check = context.Users.Where(x => x.Email == member.Email).FirstOrDefault();
            if(check == null)
            {
                String id = Guid.NewGuid().ToString();
                context.Add(new User
                {
                    Id = id,
                    Name = member.Name,
                    Email = member.Email,
                    Password = member.Password,
                    RoleId = "1"
                });
                try
                {
                   await context.SaveChangesAsync();
                    return id;
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
