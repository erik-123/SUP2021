using System;
using Firebase.Storage;
using SQLite;
using SUP2021.Models;
using SUP2021.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductViewModel();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Products>();
                    var products = conn.Table<Products>().ToList();
                    usersListView.ItemsSource = products;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Product list is empty or an error occured!", "OK");
                Console.WriteLine("Felmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("Felmeddelande");

            }

        }

        private void CursoView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            Navigation.PushAsync(new ProductDetailPage(SelectedPerson.ProductId));
        }

        private async void OnDeleteAllClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Warning", "All product will be deleted", "Confirm", "Cancel");


            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.DropTable<Products>();
                    var task = new FirebaseStorage("sup2021-c58ec.appspot.com")
                    .Child("ProductImages").DeleteAsync();

                    
            }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Product list is empty or an error occured!", "OK");
                Console.WriteLine("Felmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("Felmeddelande");

            }

        }
        private Products SelectedPerson => (Products)usersListView.SelectedItem;

    }
}
        


