using System;
using Xamarin.Forms;
using AppTest.Views;
using Xamarin.Forms.Xaml;
using Plugin.FirebasePushNotification;
using DLToolkit.Forms.Controls;
using AppTest.Database;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AppTest
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();

            MainPage = new MainPage();
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }

            };
            FlowListView.Init();
        }

		protected override void OnStart ()
		{
            SQLiteRepository.init();
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
