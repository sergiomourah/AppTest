using AppTest.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppTest.Database
{
    public class PedidoDB : IDisposable
    {
        private SQLiteConnection connection;

        public void Inserir(Pedido pedido)
        {
            connection.Insert(pedido);
        }

        public void Update(Pedido pedido)
        {
            connection.Update(pedido);
        }

        public void Delete(Pedido pedido)
        {
            connection.Delete(pedido);
        }

        public Pedido GetPedido(string id)
        {
            return connection.Table<Pedido>().FirstOrDefault(p => p.Id.Equals(id.Trim()));
        }

        public List<Pedido> GetPedidos()
        {
            return connection.Table<Pedido>().ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public PedidoDB()
        {
            connection = Connection.ConnectionBD.CriarConexao();
            connection.CreateTable<Pedido>();
        }
    }
}
