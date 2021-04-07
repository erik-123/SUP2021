﻿using Firebase.Database;
using Firebase.Database.Query;
using SUP2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/");
        public Guid productId { get;}

        public ObservableCollection<Products> Items { get; set; } = new ObservableCollection<Products>();


        public ProductDetailPage(Guid productId)
        {
            InitializeComponent();
            this.productId = productId;


           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await GetProduct(productId);
            var product = await GetProduct(productId);
            //var product = await GetAllProducts();
            
                

            productDetailListView.ItemsSource = product;
            

           // Console.WriteLine(ProductName);
            Console.WriteLine("Här visas id:t"+ productId);
            //Console.WriteLine(product.PID + product.ProductName + product.Price);

        }

      
        public async Task DeleteTheProduct(Guid productId)
        {
            var toDeletePerson = (await firebase
                .Child("Persons")
                .OnceAsync<Products>()).FirstOrDefault(a => a.Object.ProductId == productId);
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();
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
                    PID = item.Object.PID,
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