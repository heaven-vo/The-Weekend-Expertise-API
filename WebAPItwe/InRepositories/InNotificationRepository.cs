using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.InRepositories
{
    public interface InNotificationRepository
    {
        Task<List<string>> getUserToken(List<string> listUserId);
        Task<object> SaveNotification(List<string> listUserId, string image,string title, string content, string sessioonId);
        Task<object> LoadNotification(string userId, int pageIndex, int pageSize);
    }
}
