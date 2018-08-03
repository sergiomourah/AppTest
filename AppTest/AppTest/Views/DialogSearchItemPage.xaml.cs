using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DialogSearchItemPage : ContentPage
	{
        public string Filter { get; set; }
		public DialogSearchItemPage ()
		{
			InitializeComponent ();
            this.Filter = string.Empty;
            BindingContext = this;
		}

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Done_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "SearchItem", Filter);
            await Navigation.PopModalAsync();
        }
    }
}