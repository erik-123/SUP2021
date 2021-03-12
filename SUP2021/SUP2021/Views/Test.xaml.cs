using System;
using System.Linq;
using SUP2021;
using SUP2021.Models;
using Xamarin.Forms;
using SUP2021.Views;

namespace SUP2021.Views
{
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage.
            await Shell.Current.GoToAsync(nameof(Test2));
           //await Shell.Current.GoToAsync("Test2");
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(Test2)}?{nameof(Test2.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}