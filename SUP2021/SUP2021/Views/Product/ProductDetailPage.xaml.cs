using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using SUP2021.Models;
using SUP2021.Views.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/");
        public Guid productId { get; }
        public Guid ShoppingId { get; set; }

        public ObservableCollection<Products> Items { get; set; } = new ObservableCollection<Products>();


        public ProductDetailPage(Guid productId)
        {
            InitializeComponent();
            this.productId = productId;
            this.ShoppingId = Guid.NewGuid();



        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await GetProduct(productId);
            var product = await GetProduct(productId);
            //var product = await GetAllProducts();



            productDetailListView.ItemsSource = product;


            // Console.WriteLine(ProductName);
            Console.WriteLine("Här visas id:t" + productId);
            //Console.WriteLine(product.PID + product.ProductName + product.Price);

        }


        public async Task DeleteTheProduct(Guid productId)
        {
            var toDeletePerson = (await firebase
                .Child("Persons")
                .OnceAsync<Products>()).FirstOrDefault(a => a.Object.ProductId == productId);
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();
        }

        public async void OnAddBasketButton_Clicked(object sender, EventArgs e)
        {


            var value = Application.Current.Properties["Username"].ToString();
            Console.WriteLine(value);

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                //int rowsAdded = conn.Insert(newShoppingCartModel);
                // Console.WriteLine(rowsAdded);
                var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();


                var Rows = new ObservableCollection<User>();
                Rows.Clear();



                foreach (var entry in useridcheck)
                {
                    // var test = "***"; 

                    // entryList just contains values I use to populate row info 
                    var row = new User();
                    row.UID = entry.UID;
                    Console.WriteLine("test av UID: " + entry.UID);
                    var uid = entry.UID;

                    Rows.Add(row);




                    var newShoppingCartModel = new ShoppingCartModel
                    {

                        UID = uid,
                        ShoppingId = ShoppingId,
                        RefProductID = productId




                    };
                }
            }
            await Navigation.PushAsync(new ProductPage()); //Basket


        }



        private async void BtnDeleteProductClicked(object sender, EventArgs e)
        {
            //await DeleteTestProduct(1);
            Console.WriteLine("Test av valet: " + SelectedPerson.ProductId);
            await DeleteTheProduct(SelectedPerson.ProductId);
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            var allPersons = await GetAllProducts();
            productDetailListView.ItemsSource = allPersons;



        }

        public async void BtnSmsToSeller(object sender, EventArgs e)
            {
            
                   //await  Navigation.PushAsync(new SendSMStoSellerPage());
            try
            {


                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Products>();
                    var products = conn.Table<Products>().ToList();

                    foreach (var entry in products)
                    {

                        int pid = entry.RefUserID;
                        Console.WriteLine("test av URL: " + entry.RefUserID);

                        conn.CreateTable<User>();
                        //var data = conn.Table<User>().ToList();
                        var checkquery = conn.Table<User>().Where(a => a.UID == pid).ToList();


                        foreach (var entries in checkquery)
                        {


                            var number = entries.nummber;


                            await Sms.ComposeAsync(new SmsMessage
                            {
                                //Body = EntryMessage.Text,
                                Recipients = new List<string> { number }


                            });

                        }
                        }
                    }
                }
            catch (Exception)

            {
                Console.WriteLine("Device is missing sms-client");


            }







        }




        public static async Task<List<Products>> GetAllProducts()
        {
            try
            {
                var userlist = (await firebase
                .Child("Products")
                .OnceAsync<Products>()).Select(item =>
                new Products
                {
                    ProductId = item.Object.ProductId,
                    //PID = item.Object.PID,
                    Price = item.Object.Price,
                    ProductName = item.Object.ProductName,
                    URL = item.Object.URL
                }).ToList();
                return userlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

         
        public static async Task<List<Products>> GetProduct(Guid productId)
        {
            try
            {
                var allProducts = await GetAllProducts();
                await firebase
                .Child("Products")
                .OnceAsync<Products>();
                return allProducts.Where(a => a.ProductId == productId).ToList();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

       private Products SelectedPerson => (Products)productDetailListView.SelectedItem;
    }
}