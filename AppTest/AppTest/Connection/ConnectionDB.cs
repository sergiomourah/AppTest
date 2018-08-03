using SQLite.Net;
using SQLite.Net.Interop;
using System.IO;
using Xamarin.Forms;

namespace AppTest.Connection
{
    public static class ConnectionBD
    {
        public static SQLiteConnection CriarConexao()
        {
            var config = DependencyService.Get<IConnectionBD>();
            if (config != null)
                return new SQLiteConnection(config.Plataforma, Path.Combine(config.DiretorioSQLite, "apptest.db3"));
            else return null;
        }
    }
}
