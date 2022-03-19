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
    [Route("api/v1/session-management")]
    [ApiController]
    public class DemandController : ControllerBase
    {
        private readonly InMemberSessionRepository inMemberSessionRepository;
        private readonly InSessionRepository sessionRepository;
        private readonly InNotificationRepository inNotificationRepository;
        private readonly INotificationService _notificationService;


        public DemandController(InMemberSessionRepository inMemberSessionRepository, InSessionRepository sessionRepository, InNotificationRepository inNotificationRepository, INotificationService notificationService)
        {
            this.inMemberSessionRepository = inMemberSessionRepository;
            this.sessionRepository = sessionRepository;
            this.inNotificationRepository = inNotificationRepository;
            _notificationService = notificationService;
        }
        /// <summary>
        /// Member push a request to join existed session
        /// </summary>
        [HttpPost("{sessionId}/members/{memberId}/join")]
        public async Task<ActionResult> JoinSession (string sessionId, string memberId)
        {
            string title = "Yêu cầu tham gia";
            try
            {
                //get noti include: list user id and notification content
                NotificationContentModel noti = await inMemberSessionRepository.JoinSession(memberId, sessionId);
                //save notification to database
                await inNotificationRepository.SaveNotification(noti.listUserId, title, noti.content);

                //push notification to user's deviece
                var listToken = await inNotificationRepository.getUserToken(noti.listUserId);
                var notificationModel = new NotificationModel {DeviceId = listToken, Title = "Toad learn", Body= noti.content };
                var result = await _notificationService.SendNotification(notificationModel);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }           
        }
        /// <summary>
        /// Leader of session accept request of memberId
        /// </summary>
        [HttpPut("{sessionId}/members/{memberId}/accept")]
        public async Task<ActionResult> AcceptMember(string sessionId, string memberId)
        {
            try
            {
                await inMemberSessionRepository.AcceptMember(memberId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }

        /// <summary>
        /// Leader of session accept request of memberId
        /// </summary>
        [HttpPut("{sessionId}/members/{memberId}/cancel")]
        public async Task<ActionResult> CancelMember(string sessionId, string memberId)
        {
            try
            {
                await inMemberSessionRepository.CancelMember(memberId, sessionId);
                return NoContent();
            }
            catch
            {
                return Conflict();
            }
        }

        /// <summary>
        /// Cafe accept request of session
        /// </summary>
        [HttpPut("{sessionId}/mentors/{mentorId}/accept")]
        public async Task<ActionResult> AcceptSessionByCafe(string sessionId, string mentorId)
        {
            try
            {
                await sessionRepository.AcceptSessionByMentor(mentorId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Cafe accept request of session
        /// </summary>
        [HttpPut("{sessionId}/mentors/{mentorId}/cancel")]
        public async Task<ActionResult> CancelSessionByCafe(string sessionId, string mentorId)
        {
            try
            {
                await sessionRepository.CancelSessionByMentor(mentorId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
