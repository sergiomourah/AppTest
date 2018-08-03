using AppTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTest.ViewModels
{
    public  class ListMediaViewModel : BaseViewModel
    {
        public Pedido Pedido { get; set; }
        public ListMediaViewModel(Pedido pedido = null)
        {
            Pedido = pedido;
        }
    }
}
