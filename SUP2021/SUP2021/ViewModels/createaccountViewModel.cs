using SUP2021.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SUP2021.ViewModels
{
    public class createaccountViewModel : BaseViewModel
    {
        public Command registedCommand{ get; }
       public Command regCommand { get; }
        public Command logInCommand { get; }


        public createaccountViewModel()
        {
            registedCommand = new Command(OnRegistedClicked);
            regCommand = new Command(OnRegClicked);
            logInCommand = new Command(LoginClicked);
        }
        private async void LoginClicked(object obj)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginPage());//koppling till Loginpage
        }

        private async void OnRegistedClicked(object obj)
        {
            //Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(createaccount)}");
        }
       private async void OnRegClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

    }
}