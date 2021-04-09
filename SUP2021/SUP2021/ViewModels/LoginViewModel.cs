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
        public Command createCommand { get; }

        public LoginViewModel()
        {
            createCommand = new Command(createClicked);
           
        }
        private async void createClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(createaccount)}");
        }

          

  
    }
}
