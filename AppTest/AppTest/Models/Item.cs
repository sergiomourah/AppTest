using System;

namespace AppTest.Models
{
    public class Pedido
    {
        public string Id { get; set; }

        public string Cliente { get; set; }

        public decimal Valor { get; set; }

        public string Produto { get; set; }

        public int IdVendedor { get; set; }

        public string img { get; set; }
    }
}