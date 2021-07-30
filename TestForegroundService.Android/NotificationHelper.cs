using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace TestForegroundService.Droid
{
    class NotificationHelper
    {
        private static string foregroundChannelId = "9001";
        private static Context context = global::Android.App.Application.Context;

        public static Notification ReturnNotif(string message, string titre = "Your Title")
        {
            // Building intent
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            var notifBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle(titre)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.MetroIcon)
                .SetOnlyAlertOnce(true)
                .SetOngoing(true)
                .SetContentIntent(pendingIntent);

            // Building channel if API verion is 26 or above
            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                var notifManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notifManager != null)
                {
                    notifBuilder.SetChannelId(foregroundChannelId);
                    notifManager.CreateNotificationChannel(notificationChannel);
                }
            }
            return notifBuilder.Build();
        }

        public static void updateNotification(int NOTIF_ID, string text)
        {    
            Notification notification = ReturnNotif(text);
            NotificationManager mNotificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            mNotificationManager.Notify(NOTIF_ID, notification);
        }
        public static void CancelNotification()
        {
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.CancelAll();
        }
    }
}
