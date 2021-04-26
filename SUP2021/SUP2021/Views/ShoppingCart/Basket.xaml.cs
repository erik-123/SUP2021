using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUP2021.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Storage;
using Firebase;
using Firebase.Database.Query;
using Firebase.Database;
using SUP2021.ViewModels;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/%22");
        public Basket()
        {
            InitializeComponent();
            this.BindingContext = new BasketViewModel();
        }
      //  public async Task<List<Products>> GetAllofTheProducts()
       // {

          //  return (await firebase
             // .Child("Persons")
            //  .OnceAsync<Products>()).Select(item => new Products
           //   {
              //    ProductName = item.Object.ProductName,
             //     PID = item.Object.PID
             // }).ToList();
       // }
        public static async Task<List<Products>> GetAllProducts()
        {
            try
            {
                var userlist = (await firebase
                .Child("Products")
                .OnceAsync<Products>()).Select(item =>
                new Products
                {
                    Price = item.Object.Price,
                    ProductName = item.Object.ProductName
                }).ToList();
                return userlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var AllProducts = await GetAllProducts();
            lstPersons.ItemsSource = AllProducts;


        }
    }
}
