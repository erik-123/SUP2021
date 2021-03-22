using SQLite;
using SUP2021.Models;
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
    public partial class TestUserDB : ContentPage
    {
        public TestUserDB()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns              
                    var users = conn.Table<User>().ToList();
                    usersListView.ItemsSource = users;
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

            //collectionView.ItemsSource = await App.Database.GetUsersAsync();
            //var test = await App.Database.GetUsersAsync();
            
            //List<User> data = await App.Database.GetUsersAsync();
           

            //Console.WriteLine("testmeddelande");

            //foreach (User aPart in data)
            //{
                
            //    Console.WriteLine(aPart);
            //    Console.WriteLine("Listan är tom");
            //}
            //var query = App.Database.GetNotesAsync();
            //query.Wait();
            //List<Note> datas = query.Result;
            //Console.WriteLine("Total Records in Students Table are:" + " " + datas.Count);
            //Console.ReadLine();



            //for (int ii = 0; ii < test.Count; ii++)
            //{
            //    Console.WriteLine("Battery " + test[ii].Username);
            //}


            //Console.WriteLine(test);
            //Console.WriteLine(data);
            //Console.WriteLine("testmeddelande");
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            //Navigate to the NoteEntryPage.
            await Shell.Current.GoToAsync(nameof(Test2));
            await Shell.Current.GoToAsync("Test2");
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                User note = (User)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(Test2)}?{nameof(Test2.ItemId)}={note.UID.ToString()}");
            }
        }

    }
}