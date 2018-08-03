using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTest.Connection
{
    public interface IConnectionBD
    {
        string DiretorioSQLite { get; }
        ISQLitePlatform Plataforma { get; }
    }
}
