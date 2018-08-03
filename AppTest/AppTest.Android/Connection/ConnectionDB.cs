using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppTest.Connection;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppTest.Droid.Connection.ConnectionBD))]
namespace AppTest.Droid.Connection
{
    public class ConnectionBD : IConnectionBD
    {
        private string _diretorioSQLite;
        public string DiretorioSQLite
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioSQLite))
                    _diretorioSQLite = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return _diretorioSQLite;
            }
        }
        private ISQLitePlatform _plataforma;
        public ISQLitePlatform Plataforma
        {
            get
            {
                if (_plataforma == null)
                    _plataforma = new SQLitePlatformAndroid();
                return _plataforma;
            }
        }
    }
}