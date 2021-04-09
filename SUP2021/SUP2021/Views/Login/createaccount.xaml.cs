using System;
using System.Collections.Generic;
using System.Diagnostics;
using SQLite;
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
            int uid = 1;

            // users.email = email.Text;
            //users.password = password.Text;
            //users.nummber = phone.Text;

            try
            {
               
                var newuser = new User
                {
                    UID = uid,
                    firstname = name,
                    Username = user,
                    sername=Surname,
                    email=Email,
                    password=Password,
                    nummber=Phone,
                    adress=Adress
                   
            };
                using(SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>();
                    int rowsAdded = conn.Insert(newuser);
                    Console.WriteLine(rowsAdded);
                
                
                }

                //Console.WriteLine(newuser.firstname);
                //  await App.Database.AddUser(newuser);
                //          //  await database.AddUser(users);
                await DisplayAlert("Grattis", newuser.firstname + newuser.email + newuser.adress + newuser.UID + newuser.nummber, "OK");
                //Console.WriteLine("testmeddelande");
                //var query = App.Database.GetUsersAsync();
                //query.Wait();
                //List<User> datas = query.Result;
                //Console.WriteLine("Total Records in the Notedatabase Table are:" + " " + datas.Count);

                

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Something went wrong!", "OK");
               // Debug.WriteLine(ex);
                Console.WriteLine("testmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("testmeddelande");
            }
            
            await Navigation.PushAsync(new LoginPage());
        }
    }
}








