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
                await inNotificationRepository.SaveNotification(noti.listUserId, noti.image,title, noti.content, sessionId);

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
        /// Member leave session
        /// </summary>
        [HttpPost("{sessionId}/members/{memberId}/leave")]
        public async Task<ActionResult> LeaveSession(string sessionId, string memberId)
        {
            string title = "Hủy tham gia";
            try
            {
                NotificationContentModel noti = await inMemberSessionRepository.LeaveSession(memberId, sessionId);
                await inNotificationRepository.SaveNotification(noti.listUserId, noti.image,title, noti.content, sessionId);

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
        /// Leader of session accept request of memberId
        /// </summary>
        [HttpPut("{sessionId}/members/{memberId}/accept")]
        public async Task<ActionResult> AcceptMember(string sessionId, string memberId)
        {
            string title = "Yêu cầu đã được xác nhận";
            try
            {
                NotificationContentModel noti = await inMemberSessionRepository.AcceptMember(memberId, sessionId);
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
        /// Leader of session reject request of memberId
        /// </summary>
        [HttpDelete("{sessionId}/members/{memberId}/reject")]
        public async Task<ActionResult> RejectMember(string sessionId, string memberId)
        {
            string title = "Yêu cầu đã bị từ chối";
            try
            {
                NotificationContentModel noti = await inMemberSessionRepository.RejectMember(memberId, sessionId);
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
        /// Mentor accept request of session
        /// </summary>
        [HttpPut("{sessionId}/mentors/{mentorId}/accept")]
        public async Task<ActionResult> AcceptSessionByMentor(string sessionId, string mentorId)
        {
            string title = "Meetup được xác nhận";
            try
            {
                NotificationContentModel noti = await sessionRepository.AcceptSessionByMentor(mentorId, sessionId);
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
        /// Mentor reject request
        /// </summary>
        [HttpPut("{sessionId}/mentors/{mentorId}/reject")]
        public async Task<ActionResult> RejectSessionByMentor(string sessionId, string mentorId)
        {
            string title = "Meetup bị từ chối";
            try
            {
                NotificationContentModel noti = await sessionRepository.RejectSessionByMentor(mentorId, sessionId);
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
        /// Mentor or Member destroy session
        /// </summary>
        [HttpPut("{sessionId}/user/{userId}/cancel")]
        public async Task<ActionResult> CancelSession(string sessionId, string userId)
        {
            string title = "Meetup bị hủy";
            try
            {
                NotificationContentModel noti = await sessionRepository.CancelSession(userId, sessionId);
                await inNotificationRepository.SaveNotification(noti.listUserId, noti.image,title, noti.content, sessionId);

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
        /// Mentor confirm the meetup is completed
        /// </summary>
        [HttpPut("{sessionId}/mentor/{mentorId}/complete")]
        public async Task<ActionResult> CompleteSession(string sessionId, string mentorId)
        {
            try
            {
                await sessionRepository.CompleteSession(mentorId, sessionId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
