using System;
using System.Collections.Generic;
using SUP2021.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class createaccount : ContentPage
    {
        public createaccount()
        {
            InitializeComponent();
            this.BindingContext = new createaccountViewModel();
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}








