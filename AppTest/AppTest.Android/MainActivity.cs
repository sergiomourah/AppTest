﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Media;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Android.Content;

namespace AppTest.Droid
{
    [Activity(Label = "AppTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            await CrossMedia.Current.Initialize();

            CrossCurrentActivity.Current.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
    }
}

