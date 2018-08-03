using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace AppTest.Models
{
    public class Pedido : AbstractEntity
    {
        [JsonProperty("Cliente")]
        [MaxLength(144), Column("Cliente")]
        public string Cliente { get; set; }


        [JsonProperty("Valor")]
        [Column("Valor")]
        public decimal Valor { get; set; }

        [JsonProperty("Produto")]
        [MaxLength(144), Column("Produto")]
        public string Produto { get; set; }

        [JsonProperty("Img")]
        [Column("Img")]
        public string img { get; set; }

        [JsonProperty("lMedia")]
        [Ignore]
        public ObservableCollection<Media> lMedia { get; set; } = new ObservableCollection<Media>();

        [Ignore]
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