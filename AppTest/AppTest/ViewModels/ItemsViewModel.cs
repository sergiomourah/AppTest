using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppTest.Models;
using AppTest.Views;
using AppTest.Database;
using System.Collections.Generic;

namespace AppTest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Pedido> _pedidos;

        public ObservableCollection<Pedido> Pedidos
        {
            get { return _pedidos; }
            set { _pedidos = value; OnPropertyChanged(); }
        }

        public Command LoadItemsCommand { get; set; }
        public Command DialogSearchItem { get; set; }

        public ItemsViewModel()
        {
            Title = "Consulta Pedidos";
            _pedidos = new ObservableCollection<Pedido>();
            LoadItemsCommand = new Command( async() => ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Pedido>(this, "AddItem", (obj, item) =>
            {
                var _item = item as Pedido;
                _pedidos.Add(_item);
                DataStore.AddPedidoAsync(_item);
            });

            MessagingCenter.Subscribe<DialogSearchItemPage, string>(this, "SearchItem", (obj, cliente) =>
            {
                var _cliente = cliente.ToString();
                DataStore.GetPedidoByClienteAsync(_cliente);
            });
        }

        private async void ExecuteLoadItemsCommand()
        {
            try
            {
                _pedidos.Clear();
                ObservableCollection<Pedido> items = await DataStore.GetPedidosAsync(true);
                foreach (var item in items)
                {
                    _pedidos.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}