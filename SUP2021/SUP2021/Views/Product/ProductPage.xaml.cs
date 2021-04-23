using System;
using System.Collections.ObjectModel;
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
        public Guid ShoppingId { get; set; }
        public Guid productId { get; }
        public ProductPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductViewModel();
            this.ShoppingId = Guid.NewGuid();
           

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
       

    /* public async void OnAddBasketButton_Clicked (object sender, EventArgs e)
         {
         var value = Application.Current.Properties["Username"].ToString();
         var test = SelectedPerson.ProductId;




         var newShoppingCartModel = new ShoppingCartModel
         {

             //UID = uid,
            // ShoppingId = ShoppingId,
             //ProductId = test




     };
         using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
         {
             conn.CreateTable<User>();
             int rowsAdded = conn.Insert(newShoppingCartModel);
             Console.WriteLine(rowsAdded);
             var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();


             var hej = (Products)usersListView.SelectedItem;
             Guid testo = hej.ProductId;
             Console.WriteLine("Test av produktID" + testo);




             var Rows = new ObservableCollection<User>();
         Rows.Clear();



         foreach (var entry in useridcheck)
         {
             // var test = "***"; 

             // entryList just contains values I use to populate row info 
             var row = new User();
             row.UID = entry.UID;
             Console.WriteLine("test av UID: " + entry.UID);

             Rows.Add(row);
         }
         }
     }
    */





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
                    usersListView.ItemsSource = null;




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
        


