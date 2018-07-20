using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using AppTest.Models;
using Firebase.Database;
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
            ListenerPedidos();
        }

        private void ListenerPedidos()
        {
            _pedidos.Clear();
            _firebaseClient
                .Child("pedidos")
                 .AsObservable<Pedido>()
                .Subscribe(d =>
                {
                    if (d.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (d.Object.IdVendedor == 0)
                            AdicionarPedido(d.Key, d.Object);
                        else
                            RemoverPedido(d.Key);

                    }
                    else if (d.EventType == FirebaseEventType.Delete)
                    {
                        RemoverPedido(d.Key);
                    }
                });
        }

        private void AdicionarPedido(string key, Pedido pedido)
        {
            _pedidos.Add(new Pedido()
            {
                Id = key,
                Cliente = pedido.Cliente,
                Produto = pedido.Produto,
                Valor = pedido.Valor
            });
        }

        private void RemoverPedido(string pedidoKey)
        {
            var pedido = _pedidos.FirstOrDefault(x => x.Id == pedidoKey);
            _pedidos.Remove(pedido);
        }

        public async Task<bool> AddItemAsync(Pedido pedido)
        {
            _pedidos.Add(pedido);
            string dataPedido = JsonConvert.SerializeObject(pedido);
            await  _firebaseClient
                   .Child("pedidos")
                   .PostAsync(dataPedido);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Pedido pedido)
        {
            var _pedido = _pedidos.Where((Pedido arg) => arg.Id == pedido.Id).FirstOrDefault();
            _pedidos.Remove(_pedido);
            _pedidos.Add(pedido);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _pedido = _pedidos.Where((Pedido arg) => arg.Id == id).FirstOrDefault();
            _pedidos.Remove(_pedido);

            return await Task.FromResult(true);
        }

        public async Task<Pedido> GetItemAsync(string id)
        {
            return await Task.FromResult(_pedidos.FirstOrDefault(s => s.Id == id));
        }

        public ObservableCollection<Pedido> GetItemsAsync(bool forceRefresh = false)
        {
            return _pedidos;
        }
    }
}