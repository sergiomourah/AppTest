using System;

using AppTest.Models;

namespace AppTest.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Pedido Pedido { get; set; }
        public ItemDetailViewModel(Pedido pedido = null)
        {
            Title = pedido?.Id;
            Pedido = pedido;
        }
    }
}
