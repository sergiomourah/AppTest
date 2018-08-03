using AppTest.Database;
using AppTest.Models;
using AppTest.Rezise;
using AppTest.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial  class ListMedia : ContentPage
	{
        private ListMediaViewModel viewModel;
        public ListMedia (ListMediaViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Buscar Medias do Pedido
            viewModel.Pedido.lMedia = SQLiteRepository.query<Media>("SELECT * FROM " + typeof(Media).Name + " where pedido_id = " + viewModel.Pedido.Id.ToString()).Result;
            foreach (var media in viewModel.Pedido.lMedia)
            {
                var image = new Image();
                byte[] byteArray = Convert.FromBase64String(media._file);
                float size = Device.RuntimePlatform == Device.UWP ? 120 : 240;
                byte[] resizedImage = await ImageResizer.ResizeImage(byteArray, size, size);
                image.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
                wrapLayout.Children.Add(image);
            }
        }
    }
}