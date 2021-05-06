using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public string value5 { get; set; }
        public ProductPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductViewModel();
            this.ShoppingId = Guid.NewGuid();
            this.value5 = value5;


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
                    categorypicker.ItemsSource = conn.Table<CategoryModel>().ToList();






                }
            }
            catch (NullReferenceException e)
            {

                Console.WriteLine(e);
            }



        }




        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Products>();
                conn.CreateTable<CategoryModel>();
                var categories = conn.Table<CategoryModel>().ToList();
                var products = conn.Table<Products>().ToList();


                foreach (var z in products)
                {
                    foreach (var v in categories)
                    {
                        var test = categorypicker.SelectedItem;
                        var categoryid = (CategoryModel)categorypicker.SelectedItem;


                        if (categorypicker.SelectedItem == null)
                        {
                            Console.WriteLine("Fel!");

                        }
                        else if (categoryid.CategoryName != null)
                        {
                            value5 = categoryid.CategoryName;



                            if (v.CategoryName == value5 && value5 != null) //fel
                            {
                                var item2 = products.Where(s => s.CategoryId == v.CategoryId && v.CategoryId == categoryid.CategoryId);
                                //var item3 = categories.Where(s => s.CategoryName == categoryid.CategoryName);
                                usersListView.ItemsSource = item2;


                            }
                        }

                    }

                }

            }
        }





        protected override void OnDisappearing()
        {
            value5 = "value";
            categorypicker.SelectedItem = null;
            categorypicker.SelectedIndex = -1;
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


        async void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    var products = conn.Table<Products>().ToList();
                    usersListView.ItemsSource = products.Where(s => s.ProductName.Contains(ProductSearch.Text));



                }

            }

            catch (Exception ep)
            {
                Console.WriteLine(ep);
                await DisplayAlert("Alert", "Something went wrong!", "OK");
            }

        }

        private async void displayActionSheetBtn_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Products>();
                var products = conn.Table<Products>().ToList();

                var actionSheet = await DisplayActionSheet("Title", "Cancel", null, "A-Z", "Z-A", "Price: High-Low", "Price: Low-High");

                switch (actionSheet)
                {
                    case "Cancel":

                     

                        break;

                    case "A-Z":

                       var item1= products.OrderBy(w => w.ProductName).ToList();
                        usersListView.ItemsSource = item1;

                        break;

                    case "Z-A":

                       var item2= products.OrderByDescending(w => w.ProductName).ToList();
                        usersListView.ItemsSource = item2;

                        break;
                    case "Price: High-Low":

                     var item3= products.OrderBy(w => w.Price.Max()).ToList();
                        usersListView.ItemsSource = item3;
                      
                        break;

                    case "Price: Low-High":

                     var item4= products.OrderByDescending(w => w.Price.Min()).ToList();
                        usersListView.ItemsSource = item4;



                        break;


                }

            }

        }

    }
}
        


