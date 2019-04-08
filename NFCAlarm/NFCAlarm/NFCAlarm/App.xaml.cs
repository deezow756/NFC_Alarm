using Android.Content;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NFCAlarm
{
    public partial class App : Application
    {
        public App(bool startAlarm)
        {
            InitializeComponent();
            DependencyService.Register<INfcInterface>();
            DependencyService.Register<IRingtoneInterface>();
            if (startAlarm)
            {
                MainPage = new NavigationPage(new AlarmPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
