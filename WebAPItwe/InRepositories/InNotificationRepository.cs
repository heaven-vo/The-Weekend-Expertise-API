using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.InRepositories
{
    public interface InNotificationRepository
    {
        Task<object> SaveNotification(List<string> listUserId);
    }
}
