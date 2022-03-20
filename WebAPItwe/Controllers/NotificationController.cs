using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;
using WebAPItwe.Services;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService2 _notificationService;
        private readonly InNotificationRepository inNotificationRepository;

        public NotificationController(INotificationService2 notificationService, InNotificationRepository inNotificationRepository)
        {
            _notificationService = notificationService;
            this.inNotificationRepository = inNotificationRepository;
        }
        /// <summary>
        /// Load notification by user id
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> SendNotification(string userId, int pageIndex, int pageSize)
        {
            var result = await inNotificationRepository.LoadNotification(userId, pageIndex, pageSize);
            return Ok(result);
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
