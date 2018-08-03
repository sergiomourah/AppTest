using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppTest.Database;
using AppTest.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(AppTest.Services.MockDataStore))]
namespace AppTest.Services
{
    public class MockDataStore : IDataStore<Pedido>
    {
        private readonly string ENDERECO_FIREBASE = "https://demoapp-18599.firebaseio.com/";

        private ObservableCollection<Pedido> _pedidos;

        private readonly FirebaseClient _firebaseClient;

        public MockDataStore()
        {
            _firebaseClient = new FirebaseClient(ENDERECO_FIREBASE);
            _pedidos = new ObservableCollection<Pedido>();
        }

        private async Task<ObservableCollection<Pedido>> ListenerPedidos()
        {
            return await SQLiteRepository.query<Pedido>("SELECT * FROM " + typeof(Pedido).Name);
        }

        //private void AdicionarPedido(Pedido pedido)
        //{
        //    _pedidos.Add(new Pedido()
        //    {
        //        Id = Convert.ToDecimal(key)l,
        //        Cliente = pedido.Cliente,
        //        Produto = pedido.Produto,
        //        Valor = pedido.Valor,
        //        img = pedido.img
        //    });
        //}

        private void RemoverPedido(string pedidoKey)
        {
            //var pedido = _pedidos.FirstOrDefault(x => x.Id == pedidoKey);
            //_pedidos.Remove(pedido);
        }

        public async Task<bool> AddPedidoAsync(Pedido pedido)
        {
            _pedidos.Add(pedido);
            //string dataPedido = JsonConvert.SerializeObject(pedido);

            SQLiteRepository.inserir<Pedido>(pedido);
            pedido.lMedia.ToList().ForEach(media =>
            {
                media.PedidoId = pedido.Id;
                SQLiteRepository.inserir<Media>(media);
            });
            return await Task.FromResult(true);
            //await _firebaseClient
            //       .Child("pedidos")
            //       .PostAsync(dataPedido);
        }

        public async Task<bool> UpdatePedidoAsync(Pedido pedido)
        {
            var _pedido = _pedidos.Where((Pedido arg) => arg.Id == pedido.Id).FirstOrDefault();
            _pedidos.Remove(_pedido);
            _pedidos.Add(pedido);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeletePedidoAsync(long? id)
        {
            var _pedido = _pedidos.Where((Pedido arg) => arg.Id == id).FirstOrDefault();
            _pedidos.Remove(_pedido);

            return await Task.FromResult(true);
        }

        public async Task<Pedido> GetPedidoByIdAsync(long? id)
        {
            return await Task.FromResult(_pedidos.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Pedido> GetPedidoByClienteAsync(string cliente)
        {
            return await Task.FromResult(_pedidos.FirstOrDefault(s => s.Cliente == cliente));
        }

        public async Task<ObservableCollection<Pedido>> GetPedidosAsync(bool forceRefresh = false)
        {
            //Buscar Pedidos
            Task<ObservableCollection<Pedido>> listaPedidos = ListenerPedidos();
            return await listaPedidos;
        }
    }
}