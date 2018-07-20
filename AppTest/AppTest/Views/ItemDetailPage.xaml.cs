using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppTest.Models;
using AppTest.ViewModels;

namespace AppTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}