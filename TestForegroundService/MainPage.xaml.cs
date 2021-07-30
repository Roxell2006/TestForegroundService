using System;
using Xamarin.Forms;

namespace TestForegroundService
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        void onStart(Object sender, EventArgs e)
        {
            DependencyService.Get<IServices>().StartService();
        }
        void onStop(Object sender, EventArgs e)
        {
            DependencyService.Get<IServices>().StopService();
        }
    }
}
