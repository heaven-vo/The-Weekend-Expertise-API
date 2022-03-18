using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;

namespace WebAPItwe.Repositories
{
    public class NotificationRepository : InNotificationRepository
    {
        private readonly dbEWTContext context;

        public NotificationRepository(dbEWTContext context)
        {
            this.context = context;
        }
        public async Task<object> SaveNotification(List<string> listUserId)
        {
            var listToken = new List<string>();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            foreach (var userId in listUserId)
            {
                //var token = await(from user in context.Users
                //                  join noti in context.Notifications on user.Id equals noti.UserId 
                //                  where user.Id == userId 
                //                  select noti.Id).ToListAsync();
            
            }
            return null;
        }
    }
}
