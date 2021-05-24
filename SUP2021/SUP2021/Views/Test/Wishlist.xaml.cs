using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using SUP2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wishlist : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/%22");
        private ObservableCollection<Products> _wishlist;
        public Wishlist()
        {
            InitializeComponent();
            _wishlist = new ObservableCollection<Products>();

            listWish.RefreshCommand = new Command(() => {

                RefreshData();
                listWish.IsRefreshing = false;
            });

        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<WishListModel>();
                    var products = conn.Table<WishListModel>().ToList();

                    //var Rows = new ObservableCollection<Products>();
                    _wishlist.Clear();

                    // ShoppingCart.ItemsSource = products;

                    foreach (var b in products)
                    {
                        var test = b.ReferProductID;
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

                            _wishlist.Add(row);
                            listWish.ItemsSource = _wishlist;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Alert", "Something went wrong!", "OK");


            }

        }

        public async void RefreshData()
        {

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<WishListModel>();
                    var products = conn.Table<WishListModel>().ToList();

                    //var Rows = new ObservableCollection<Products>();
                    _wishlist.Clear();

                    // ShoppingCart.ItemsSource = products;

                    foreach (var b in products)
                    {
                        var test = b.ReferProductID;
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

                            _wishlist.Add(row);
                            listWish.ItemsSource = _wishlist;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Alert", "Something went wrong!", "OK");


            }


        }

        private async void WishListTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Products;



            if (item != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<WishListModel>();
                    var shoppingcartitems = conn.Table<WishListModel>().ToList();
                    var products = conn.Table<Products>().ToList();

                    var data1 = shoppingcartitems.Where(x => x.ReferProductID == item.ProductId).FirstOrDefault();
                    var data2 = products.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                    Console.WriteLine(data2.ProductId);

                    var et = new ObservableCollection<Products>();



                    if (data1.WishId != null)
                    {

                        conn.Delete(data1);
                        await DisplayAlert("Congrats!", "Delete Successfully! Please pull to refresh", "OK");

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



        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            await DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");



        }



        public static async Task<bool> DeleteWishListModel(Guid productid)
        {
            try
            {

                var toDeletePerson = (await firebase
                .Child("Wishlist")
                .OnceAsync<WishListModel>()).Where(a => a.Object.ReferProductID == productid).FirstOrDefault();
                await firebase.Child("Wishlist").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public static async Task<List<WishListModel>> GetAllProducts()
        {
            try
            {
                var test = (await firebase
                .Child("Wishlist")
                .OnceAsync<WishListModel>()).Select(item =>
                new WishListModel
                {
                    ReferProductID = item.Object.ReferProductID,
                    WishId = item.Object.WishId,
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

        public static async Task<List<WishListModel>> GetProduct()
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
