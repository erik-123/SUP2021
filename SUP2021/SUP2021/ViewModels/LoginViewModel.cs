using SQLite;
using SUP2021.Models;
using SUP2021.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SUP2021.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command createCommand { get; }


      

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            createCommand = new Command(createClicked);
            

        }

        private async void OnLoginClicked(object obj)
        {
            //Detta kommando gör ingeting, just nu! Behöver ta bort det vid tillfälle.

           // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
           await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void createClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(createaccount)}");
        }

    }
}
