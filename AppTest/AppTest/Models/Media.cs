using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppTest.Models
{
    public class Media : AbstractEntity
    {
        [JsonProperty("pedido_id")]
        [Column("pedido_id")]
        public long? PedidoId { get; set; }

        [JsonProperty("descricao")]
        [MaxLength(144), Column("descricao")]
        public string Descricao { get; set; } 

        [Ignore]
        public ImageSource getFileResized { get; set; }

        [JsonProperty("file")]
        [Column("file")]
        public string _file { get; set; }

        [Ignore]
        public ImageSource getFile
        {
            get
            {
                if (!string.IsNullOrEmpty(_file))
                {
                    return ImageSource.FromStream(() =>
                    {
                        byte[] byteArray = Convert.FromBase64String(_file);
                        return new MemoryStream(byteArray);
                    });
                }
                else return null;
            }
        }
        [JsonProperty("TipoMedia")]
        [Column("TipoMedia")]
        public TipoMedia TipoMedia { get; set; }
    }

    public class ImageList
    {
        public List<string> Photos = null;
    }
}
