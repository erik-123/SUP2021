using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SUP2021.Models;
using System.Collections.ObjectModel;
using SUP2021.ViewModels;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Profilepage : ContentPage
    {
        private string testurl { get; }

        public Profilepage()
        {
            InitializeComponent();
            this.BindingContext = new profileViewModel();

           




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
                    // listUser.ItemsSource = useridcheck;

                    var Rows = new ObservableCollection<User>();
                    Rows.Clear();
                    // Rows is initialized as a new collection so update the itemssource
                    listUser.ItemsSource = Rows;

                    foreach (var entry in useridcheck)
                    {
                        // var test = "***"; 

                        // entryList just contains values I use to populate row info 
                        var row = new User();
                        row.URL = entry.URL;
                        row.Username = entry.Username;
                        row.firstname = entry.firstname;
                        row.sername = entry.sername;
                        row.nummber = entry.nummber;
                        Console.WriteLine("test av URL: "+ entry.URL);


                        var length = entry.password.Length;

                        //Enkel maskering av lösenordet

                        for (int i = 0; i < length; i++)
                        {
                            var input = ("*");
                            entry.password = input;
                            row.password = entry.password;
                        }



                        Console.WriteLine(entry.firstname + entry.nummber + entry.Username + entry.password);


                        Rows.Add(row);
                    }



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
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "useridcheck", "OK");
            }




           
        }

        public async void OnImageEditButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileEditPage()); //EditPage
        }

        public async void OnImageSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new settings());
        }

        public async void OnVarukorgButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Basket()); //Basket
        }

    }
}


        




