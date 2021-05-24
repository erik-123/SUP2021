using SUP2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSuccesfulPage : ContentPage
    {
      

        public PaymentSuccesfulPage()
        {
            InitializeComponent();

            this.BindingContext = this;

         }


        public async void OnButtonClosedClicked(Object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ProductPage());
            await Shell.Current.GoToAsync($"//{nameof(ProductPage)}");


        }

        public async void OnButtonPrintClicked(Object sender, EventArgs e)
        {

            await DisplayAlert("Alert","Function is not implemented yet","OK");
        }


    }
}