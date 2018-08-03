using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppTest.Models;
using Plugin.Media;
using System.IO;
using System.Text;
using Plugin.Media.Abstractions;

namespace AppTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Pedido Pedido { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            this.Pedido = new Pedido();
            BindingContext =  this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Pedido);
            await Navigation.PopModalAsync();
        }

        private async void TirarFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo",
                    CompressionQuality = 20
                });

            if (file == null)
                return;

            byte[] byteArray = File.ReadAllBytes(file.Path);
            Pedido.img = Convert.ToBase64String(byteArray);

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });

            Pedido.lMedia.Add(new Media()
            {
                Id = GetHashCode(),
                Descricao = file.Path,
                TipoMedia = TipoMedia.IMAGEM,
                _file = Convert.ToBase64String(byteArray),
                PedidoId = Pedido.Id
            });
        }

        private async void EscolherFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            byte[] byteArray = File.ReadAllBytes(file.Path);
            Pedido.img = Convert.ToBase64String(byteArray);

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });

            Pedido.lMedia.Add(new Media()
            {
                Id = GetHashCode(),
                Descricao = file.Path,
                TipoMedia = TipoMedia.IMAGEM,
                _file = Convert.ToBase64String(byteArray),
                PedidoId = Pedido.Id
            });
        }

        private async void GravarVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakeVideoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo",
                    Quality = VideoQuality.Medium
                });

            if (file == null)
                return;

            Pedido.lMedia.Add(new Media()
            {
                Id = GetHashCode(),
                Descricao = file.Path,
                TipoMedia = TipoMedia.VIDEO,
                _file = file.Path,
                PedidoId = Pedido.Id
            });
        }

        private async void EscolherVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Ops", "Galeria de videos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            Pedido.lMedia.Add(new Media()
            {
                Id = GetHashCode(),
                Descricao = file.Path,
                TipoMedia = TipoMedia.VIDEO,
                _file = file.Path,
                PedidoId = Pedido.Id
            });
        }

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var media = args.SelectedItem as Media;
                byte[] baseVideo = System.IO.File.ReadAllBytes(media._file);
                string video = Convert.ToBase64String(baseVideo);
                videoPlayer.Source = (UriVideoSource)Application.Current.Resources[media._file];
            }
        }
    }
}