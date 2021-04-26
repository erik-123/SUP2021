using SQLite;
using SUP2021.Models;
using SUP2021.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            // Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            InitializeComponent();
            
            this.BindingContext = new LoginViewModel();


            //var Appshell = new AppShell();

        }
        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns              
                    var data = conn.Table<User>().ToList();
                    // usersListView.ItemsSource = users;
   

                    var data1 = data.Where(x => x.Username == Username.Text && x.password == Password.Text).FirstOrDefault();
                    if (data1 != null)
                    {
                        Application.Current.Properties["Username"] = Username.Text;
                        await Application.Current.SavePropertiesAsync();

                        await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Wrong username or password!", "OK");
                        Console.WriteLine("Fel lösenord");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Something went wrong!", "OK");
                // Debug.WriteLine(ex);
                Console.WriteLine("testmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("testmeddelande");

            }


        }
    }
}