using Android.Content;
using TestForegroundService.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ServicesHelper))]
namespace TestForegroundService.Droid
{
    class ServicesHelper : IServices
    {
        private static Context context = Android.App.Application.Context;
        Intent intent; 

        public void StartService()
        {
            if(intent == null)
            {
                intent = new Intent(context, typeof(DataSource));

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)                
                    context.StartForegroundService(intent);               
                else
                    context.StartService(intent);               
            }
        }

        public void StopService()
        {
            if(intent != null)
            {
                context.StopService(intent);
                intent = null;
            }
        }
    }
}