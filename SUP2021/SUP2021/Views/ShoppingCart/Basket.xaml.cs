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
using SQLite;
using SUP2021.Views.Test;
using System.Collections.ObjectModel;

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

       
      
        
        protected override async void OnAppearing()
        {
            
            base.OnAppearing();

          try {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ShoppingCartModel>();
                    var products = conn.Table<ShoppingCartModel>().ToList();

                    var Rows = new ObservableCollection<Products>();
                    Rows.Clear();

                    // ShoppingCart.ItemsSource = products;

                    foreach (var b in products)
                    {
                        var test = b.RefProductID;
                        var query = conn.Table<Products>().Where(a => a.ProductId == test).ToList();
                        Console.WriteLine(query);

                        foreach (var c in query)
                        {
                            Console.WriteLine(c.ProductName);


                            var row = new Products();
                            var sum = conn.ExecuteScalar<int>("SELECT SUM(Price) FROM Products INNER JOIN ShoppingCartModel ON Products.ProductId = ShoppingCartModel.RefProductID  WHERE Price > 0");
                            



                            row.Price = c.Price;
                            row.ProductName = c.ProductName;
                            row.ProductId = c.ProductId;
                            row.URL = c.URL;
                            row.RefUserID = c.RefUserID;

                            Rows.Add(row);
                            ShoppingCart.ItemsSource = Rows;
                            CountShoppingCart.Text = sum.ToString();

                            
                        }


                     

                    }
                }
          }
            catch(Exception ex)
          {         
            Console.WriteLine(ex);
            await DisplayAlert("Alert","Something went wrong!","OK");        
               
            
           }
                        
        }
        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

            // var mi = ((MenuItem)sender);
            // await DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");


            // Guid test = new Guid();
            // await DeleteShoppingCartModel(test);
            //using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //{
           
            var ShoppinCartItemID = ShoppingCart.SelectedItem;
                var categoryid = (ShoppingCartModel)ShoppingCart.SelectedItem;
                  Console.WriteLine("Test av idt" + categoryid.ShoppingId);

            //    var data = conn.Table<ShoppingCartModel>();
            //    var data1 = data.Where(x => x.ShoppingId == categoryid.ShoppingId).FirstOrDefault();

                
            //    if (data1.ShoppingId != null)
            //    {
            //        conn.Delete(data1);
            //        await DisplayAlert("Congrats!", "Delete Successfully", "OK");
                   
            //    }
            //    else
            //    {
            //        await DisplayAlert("Alert", "No Product found!", "OK");
            //    }




            //}

        }



        public static async Task<bool> DeleteShoppingCartModel(Guid productid)
        {
            try
            {

                var toDeletePerson = (await firebase
                .Child("ShoppingCart")
                .OnceAsync<ShoppingCartModel>()).Where(a => a.Object.RefProductID == productid).FirstOrDefault();
                await firebase.Child("ShoppingCart").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        //Firebasemetod för att spara-->

        //var AllProducts = await GetAllProducts();
        //await DisplayAlert("Test av", AllProducts.ToString(), "OK");
        //var product = await GetProduct();

        //ShoppingCart.ItemsSource = AllProducts;

        //foreach (var a in AllProducts) 
        //{
        //    var test = a.UID;
        //    await DisplayAlert("Test av", test.ToString(), "OK");
        //    Console.WriteLine(product);


        //}                      




        public async void OnCheckoutClicked(object sender, EventArgs e)
        {

           await Navigation.PushAsync(new PaymentTest());

        }

        public static async Task<List<ShoppingCartModel>> GetAllProducts()
        {
            try
            {
                var test = (await firebase
                .Child("ShoppingCart")
                .OnceAsync<ShoppingCartModel>()).Select(item =>
                new ShoppingCartModel
                {
                    //RefProductID
                    //UID
                    //ShoppingId


                    RefProductID = item.Object.RefProductID,
                    ShoppingId = item.Object.ShoppingId,
                    UID = item.Object.UID
                }).ToList();


               
                
                 return test;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                string errorMessage = "Something went wrong!";
                Console.WriteLine(errorMessage);
                return null;
            }
        }

        public static async Task<List<ShoppingCartModel>> GetProduct()
        {
            try
            {
                int uid = 3;
                var allProducts = await GetAllProducts();
                await firebase
                .Child("Products")
                .OnceAsync<Products>();
                return allProducts.Where(a => a.UID == uid).ToList();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }



    }
}
