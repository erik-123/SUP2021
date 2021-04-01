using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SUP2021.Models;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class Profilepage : ContentPage
    {
      
        public Profilepage()
        {
            InitializeComponent();
          
               
               
         

         
        }
             protected override async void OnAppearing()
             {
               base.OnAppearing();
            var value = Application.Current.Properties["Username"].ToString();
            try
            {


                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns
                    var data = conn.Table<User>().ToList();
                    var checkquery = conn.Table<User>().Where(a => a.Username == value).FirstOrDefault();

                    Console.WriteLine(value);

                   
                    
                        //string s = text.Text;
                        // var skit = conn.Query<User>("SELECT Username FROM User WHERE Username = ?", value);
                        // s = skit.FirstOrDefault()?.User;


                        var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();
                        listUser.ItemsSource = useridcheck;

                        // text.Text = useridcheck;
                        //await DisplayAlert("Alert", "useridcheck", "OK");
                        string tom = string.Empty;
                        // foreach (var a in useridcheck)
                        // {
                        //   s = a.Username.ToString();
                        //  Console.WriteLine("..." + s);
                        //}
                        // text.Text = s;
                    
                    
                    NavigationPage.SetHasBackButton(this, false);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", "useridcheck", "OK");
            }
            }


       

    }
        }


        




