using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;
using WebAPItwe.Services;

namespace WebAPItwe.Controllers
{
    [Route("api/v1/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly InSessionRepository sessionRepository;
        private readonly InNotificationRepository inNotificationRepository;
        private readonly INotificationService _notificationService;

        public SessionController(InSessionRepository sessionRepository, InNotificationRepository inNotificationRepository, INotificationService notificationService)
        {
            this.sessionRepository = sessionRepository;
            this.inNotificationRepository = inNotificationRepository;
            _notificationService = notificationService;
        }
        /// <summary>
        /// Create new Session 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateNewSession(NewSessionModel newSession)
        {
            string title = "Mời tham gia";
            try
            {
                string sessionId = Guid.NewGuid().ToString();
                NotificationContentModel noti = await sessionRepository.CreateNewSession(newSession, sessionId);
                await inNotificationRepository.SaveNotification(noti.listUserId, noti.image, title, noti.content, sessionId);

                var listToken = await inNotificationRepository.getUserToken(noti.listUserId);
                var notificationModel = new NotificationModel { DeviceId = listToken, Title = "Toad learn", Body = noti.content };
                var result = await _notificationService.SendNotification(notificationModel);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }

        }
        /// <summary>
        /// Load recommend session by major of member (for home page)  
        /// </summary>
        [HttpGet("home")]
        public async Task<ActionResult> LoadRecommendSession(string memberId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadRecommendSession(memberId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load recent session in today by member Id 
        /// </summary>
        [HttpGet("today")]
        public async Task<ActionResult> LoadTodaySession(string memberId)
        {
            try
            {
                var session = await sessionRepository.LoadTodaySession(memberId);
                return Ok(session);
            }
            catch
            {
                return Conflict();
            }

        }


        /// <summary>
        /// Load all session (for search page)  
        /// </summary>
        [HttpGet()]
        public async Task<ActionResult> LoadSession(string memberId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadSession(memberId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load session by major id (for search page)  
        /// </summary>
        [HttpGet("byMajor")]
        public async Task<ActionResult> LoadSessionByMajor(string memberId, string majorId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadSessionByMajor(memberId, majorId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load my session (for my meet page)  
        /// </summary>
        [HttpGet("my-sessions")]
        public async Task<ActionResult> LoadMySession(string memberId, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadMySession(memberId, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load my session by staus (for my meet page)  
        /// </summary>
        [HttpGet("my-sessions-by-status")]
        public async Task<ActionResult> LoadMySessionByStatus(string memberId, int status, int pageIndex, int pageSize)
        {
            try
            {
                var listSession = await sessionRepository.LoadMySessionByStatus(memberId, status, pageIndex, pageSize);
                return Ok(listSession);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load detail of session
        /// </summary>
        [HttpGet("detail")]
        public async Task<ActionResult> LoadSessionDetail(string memberId, string sessionId)
        {
            try
            {
                var session = await sessionRepository.LoadSessionDetail(memberId, sessionId);
                return Ok(session);
            }
            catch
            {
                return Conflict();
            }

        }

        /// <summary>
        /// Load list request to join session (just leader see it)
        /// </summary>
        [HttpGet("{sessionId}/awaiting-members")]
        public async Task<ActionResult> LoadRequestMember(string sessionId)
        {
            var result = await sessionRepository.LoadRequestMember(sessionId);
            return Ok(result);
        }
    }
}
