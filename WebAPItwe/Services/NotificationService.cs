using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPItwe.Models;
using WebAPItwe.Setting;
using static WebAPItwe.Models.GoogleNotification;

namespace WebAPItwe.Services
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                /* FCM Sender (Android Device) */
                FcmSettings settings = new FcmSettings()
                {
                    SenderId = _fcmNotificationSetting.SenderId,
                    ServerKey = _fcmNotificationSetting.ServerKey
                };
                HttpClient httpClient = new HttpClient();

                string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                List<string> ListDeviceToken = notificationModel.DeviceId;

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DataPayload dataPayload = new DataPayload();
                dataPayload.Title = notificationModel.Title;
                dataPayload.Body = notificationModel.Body;

                GoogleNotification notification = new GoogleNotification();
                notification.Data = dataPayload;
                notification.Notification = dataPayload;

                var fcm = new FcmSender(settings, httpClient);
                foreach (var deviceToken in ListDeviceToken)
                {
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
                }
                response.IsSuccess = true;
                response.Message = "Notification sent successfully";
                return response;
                //var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);


                //    if (fcmSendResponse.IsSuccess())
                //    {
                //        response.IsSuccess = true;
                //        response.Message = "Notification sent successfully";
                //        return response;
                //    }
                //    else
                //    {
                //        response.IsSuccess = false;
                //        string error = "";
                //        for (int i = 0; i < fcmSendResponse.Results.Count; i++)
                //            error += fcmSendResponse.Results[i].Error + " ";
                //        response.Message = error;
                //        return response;
                //}            
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
