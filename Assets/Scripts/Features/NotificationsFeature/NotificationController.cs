using System;
using Unity.Notifications.Android;
//using Unity.Notifications.iOS;
using UnityEngine;

namespace Features.NotificationsFeature
{
    public class NotificationController : MonoBehaviour
    {
        #region Fields

        private const string _androidNotificationId = "TestRacingGame";
        private const string _iosNotificationID = "TestRacingGame";


        #endregion

        #region UnityMethods

        private void Awake()
        {
            CreateNotification(Application.platform);
        }

        #endregion

        #region Methods

        private void CreateNotification(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.WindowsPlayer:
                    break;
                case RuntimePlatform.WindowsEditor:
                    break;
                case RuntimePlatform.IPhonePlayer:
                    CreateNotificationIOS();
                    break;
                case RuntimePlatform.Android:
                    CreateNotificationAdnroid();
                    break;
                default:
                    break;
            }
        }

        private void CreateNotificationIOS()
        {
            //var iosNotification = new iOSNotification
            //{
            //    Identifier = _iosNotificationID,
            //    Title = "IOS Notifier",
            //    Subtitle = "Subtitle IOS Notifier",
            //    Body = "Description IOS Notifier",
            //    Data = "00/00/0000",
            //    ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound,
            //    //Trigger = new iOSNotificationTimeIntervalTrigger();
            //};

            //iOSNotificationCenter.ScheduleNotification(iosNotification);
        }

        private void CreateNotificationAdnroid()
        {
            var androidSettingsChannel = new AndroidNotificationChannel
            {
                Id = _androidNotificationId,
                Name = "Notifier",
                Description = "Description notifier",
                Importance = Importance.Low,
                CanBypassDnd = false,
                EnableLights = false,
                CanShowBadge = true,
                EnableVibration = false,
                LockScreenVisibility = LockScreenVisibility.Public
            };

            AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChannel);

            var androidNotification = new AndroidNotification
            {
                Title = "Test",
                Color = Color.black,
                RepeatInterval = TimeSpan.FromDays(1)
            };

            var sendID = AndroidNotificationCenter.SendNotification(androidNotification, _androidNotificationId);
        }

        #endregion
    }
}