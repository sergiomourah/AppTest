using System;
using System.IO;
using Xamarin.Forms;

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

        public ImageSource Imagem
        {
            get
            {            
                if (!string.IsNullOrEmpty(img))
                {
                    return ImageSource.FromStream(() =>
                    {
                        byte[] byteArray = Convert.FromBase64String(img);
                        return new MemoryStream(byteArray);
                    });
                }
                else return null;
            }

        }
    }
}