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
    public interface INotificationService2
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
    public class NotificationService2 : INotificationService2
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService2(IOptions<FcmNotificationSetting> settings)
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
                //string deviceToken = notificationModel.DeviceId;
                string deviceToken = "ddMgP8K9Ruykl9vqGUK6my:APA91bF6n-phMghTt35A4w8_VI6AT_4D5UAsFWMoaNzOVEK2P_gA0Ie5l5JEEtTlN_p4_1_bfoTUtKICXdY20lijpA45-DppeA7d0vn3n--BXxd0B7jp9iTwHL7tT8EUi8B_MAzC9GyC";
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
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}
