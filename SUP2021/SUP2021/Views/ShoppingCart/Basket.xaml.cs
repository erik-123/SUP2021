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

        private ObservableCollection<Products> _products;

        private int NewSum { get; set; }
        public Basket()
        {
            InitializeComponent();
            this.BindingContext = new BasketViewModel();

            _products = new ObservableCollection<Products>();


            ShoppingCart.RefreshCommand = new Command(() => {
                   
                RefreshData();
                ShoppingCart.IsRefreshing = false;
            });



        }
        public void RefreshData()
        {
            
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ShoppingCartModel>();
                    var products = conn.Table<ShoppingCartModel>().ToList();

                    
                    _products.Clear();
                    

                    foreach (var b in products)
                    {
                        var test = b.RefProductID;
                        var query = conn.Table<Products>().Where(a => a.ProductId == test).ToList();
                        Console.WriteLine(query);

                        foreach (var c in query)
                        {
                            Console.WriteLine(c.ProductName);


                            var row = new Products();                          




                            row.Price = c.Price;
                            row.ProductName = c.ProductName;
                            row.ProductId = c.ProductId;
                            row.URL = c.URL;
                            row.RefUserID = c.RefUserID;

                            _products.Add(row);
                            ShoppingCart.ItemsSource = _products;                            


                        }




                    }
                }
                   

        }


        protected override async void OnAppearing()
        {
            
            base.OnAppearing();

          try {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ShoppingCartModel>();
                    var products = conn.Table<ShoppingCartModel>().ToList();

                    //var Rows = new ObservableCollection<Products>();
                    _products.Clear();

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

                            Application.Current.Properties["SUM"] = sum;
                            await Application.Current.SavePropertiesAsync();


                            row.Price = c.Price;
                            row.ProductName = c.ProductName;
                            row.ProductId = c.ProductId;
                            row.URL = c.URL;
                            row.RefUserID = c.RefUserID;

                            _products.Add(row);
                            ShoppingCart.ItemsSource = _products;
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
        public void UpdateListview()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                var table = from d in conn.Table<Products>()
                            select d;

               var prods = new List<Products>();

                foreach (var prod in table)
                {
                    prods.Add(prod);
                }

                prods.AddRange(prods);
                ShoppingCart.ItemsSource = prods;

            }
        }


        private async void ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
             var item = e.SelectedItem as Products;

                NewSum = 0;           
           

              

                if (item != null)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<ShoppingCartModel>();
                        var shoppingcartitems = conn.Table<ShoppingCartModel>().ToList();
                        var products = conn.Table<Products>().ToList();  
                    
                    
                        

                        var data1 = shoppingcartitems.Where(x => x.RefProductID == item.ProductId).FirstOrDefault();
                        var data2 = products.Where(x => x.ProductId == item.ProductId).FirstOrDefault();


                        Console.WriteLine(data2.ProductId);

                        var et = new ObservableCollection<Products>();


                        if (data1.ShoppingId != null)
                        {
                        
                           var sum = conn.ExecuteScalar<int>("SELECT SUM(Price) FROM Products INNER JOIN ShoppingCartModel ON Products.ProductId = ShoppingCartModel.RefProductID  WHERE Price > 0"); 

                            conn.Delete(data1);
                            await DisplayAlert("Congrats!", "Delete Successfully! Please pull to refresh", "OK");
                            

                            int deletedvalue = int.Parse(data2.Price);
                            NewSum = sum - deletedvalue;                           

                            CountShoppingCart.Text = NewSum.ToString();

                        }
                        else
                        {
                            await DisplayAlert("Alert", "No Product found!", "OK");
                        }

                    

                }
            }

            else if (item == null)
            {
                await DisplayAlert("Alert", "Error!", "OK");
            }
        

        }


        //public async void OnListItemSelected(object sender, ItemTappedEventArgs e)
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
        //    {
        //        conn.CreateTable<ShoppingCartModel>();
        //        var shoppingcartitems = conn.Table<ShoppingCartModel>().ToList();
                
        //        var test = e.Item;
        //        var item = e.Item as ShoppingCartModel;

        //        var data1 = shoppingcartitems.Where(x => x.ShoppingId == item.ShoppingId).FirstOrDefault();


        //        if (data1.ShoppingId != null)
        //        {
        //            conn.Delete(data1);
        //            await DisplayAlert("Congrats!", "Delete Successfully", "OK");
                   

        //        }
        //        else
        //        {
        //            await DisplayAlert("Alert", "No Product found!", "OK");
        //        }


        //    }
        //}


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

        //här

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
            var value = Application.Current.Properties["SUM"].ToString();
            int price = int.Parse(value);

            if (String.IsNullOrEmpty(price.ToString()) || String.IsNullOrEmpty(CountShoppingCart.Text) || price == 0)
            {
                await DisplayAlert("Alert", "ShoppingCart is Empty!", "OK");
              
            } else
            { 
              await Navigation.PushAsync(new CountryTest());
            }

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
