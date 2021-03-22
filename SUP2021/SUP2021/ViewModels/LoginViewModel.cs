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


            //try
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //    {
            //        conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns              
            //        var data = conn.Table<User>().ToList();
            //        // usersListView.ItemsSource = users;

            //        var data1 = data.Where(x => x.Username == Username.Text && x.password == Password.Text).FirstOrDefault();
            //        if (data1 != null)
            //        {
            //            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            //        }
            //        else
            //        {
            //            await DisplayAlert("Alert", "Wrong username or password!", "OK");
            //            Console.WriteLine("Fel lösenord");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Alert", "Something went wrong!", "OK");
            //    // Debug.WriteLine(ex);
            //    Console.WriteLine("testmeddelande");
            //    Console.WriteLine(ex);
            //    Console.WriteLine("testmeddelande");

            //}


            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void createClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(createaccount)}");
        }

    }
}
