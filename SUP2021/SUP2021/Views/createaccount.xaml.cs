using System;
using System.Collections.Generic;
using System.Diagnostics;
using SUP2021.Data;
using SUP2021.Models;
using SUP2021.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class createaccount : ContentPage 
    {
        static NoteDatabase database;
        User users = new User();
        public createaccount()
        {
            InitializeComponent();
            //Firstname.ReturnCommand = new Command(() => username.Focus());
            
            this.BindingContext = new createaccountViewModel();
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            
          // users.firstname = Firstname.Text;
            //users.sername = surname.Text;
            //users.Username = username.Text;
            string name = Firstname.Text;
            string user = username.Text;
            string Surname = surname.Text;
            string Email = email.Text;
            string Password = password.Text;
            string Phone = phone.Text;
            string Adress = "appgatan 2";

            // users.email = email.Text;
            //users.password = password.Text;
            //users.nummber = phone.Text;

            try
            {
                var newuser = new User
                {
                    firstname = name,
                    Username = user,
                    sername=Surname,
                    email=Email,
                    password=Password,
                    nummber=Phone,
                    adress=Adress
                   
            };
                database.AddUser(newuser);
                //database.AddUser(users);
                await DisplayAlert("Grattis", "Tjänsten kostar 5000000 kr per användningstimme", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Tjänsten är gratis men du måste lägga in dina kortuppgifter", "OK");
                Debug.WriteLine(ex);
            }
            
            await Navigation.PushAsync(new LoginPage());
        }
    }
}








