using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestForegroundService.Droid
{
    [Service]
    public class DataSource : Service
    {
        CancellationTokenSource _cts;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public const int ServiceRunningNotifID = 9000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();
            
            Notification notif = NotificationHelper.ReturnNotif("Mon message");
            StartForeground(ServiceRunningNotifID, notif);

            // code de tâche en premier plan ici
            
            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    string time = DateTime.Now.ToString();
                    if (!_cts.IsCancellationRequested)
                        NotificationHelper.updateNotification(ServiceRunningNotifID, time);        
                }
            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();
                _cts.Cancel();
            }
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }
}