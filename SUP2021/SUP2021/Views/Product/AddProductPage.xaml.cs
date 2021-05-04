using System;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SUP2021.Models;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Firebase;
using Firebase.Storage;
using shortid;
using shortid.Configuration;
using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database.Query;
using SUP2021.Services;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        MediaFile file;

        private string Imgurl { get; set; }

        public Guid ProductId { get; set; }

       public Guid CategoryId { get; set; }
      



        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/");
        private object user;

        public AddProductPage()
        {
            InitializeComponent();
           
            this.ProductId = Guid.NewGuid();
            this.CategoryId = Guid.NewGuid();
         


            //standard bilder om ingen bild valts
            //imgBanner.Source = ImageSource.FromResource("XamarinFirebase.images.banner.png");
            //imgChoosed.Source = ImageSource.FromResource("XamarinFirebase.images.default.jpg");


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var AllProducts = await GetAllofTheProducts();

    
            





            
               try
            { 
            
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    
                {
                conn.CreateTable<CategoryModel>();

                List<CategoryModel> collection = new List<CategoryModel>();



                {
                    collection.Add(new CategoryModel() { CategoryName = "Kläder", CategoryId = Guid.NewGuid()});
                    collection.Add(new CategoryModel() { CategoryName = "Elektronik", CategoryId = Guid.NewGuid() });
                    collection.Add(new CategoryModel() { CategoryName = "Mat", CategoryId = Guid.NewGuid() });
                    collection.Add(new CategoryModel() { CategoryName = "Annat", CategoryId = Guid.NewGuid() });

                   /* collection.Add(new CategoryModel() { CategoryName = "Kläder", CategoryId = CategoryId });
                    collection.Add(new CategoryModel() { CategoryName = "Elektronik", CategoryId = CategoryId });
                    collection.Add(new CategoryModel() { CategoryName = "Mat", CategoryId = CategoryId });
                    collection.Add(new CategoryModel() { CategoryName = "Annat", CategoryId = CategoryId });*/




                };
                    
                    foreach (var x in collection)
                {
                    if (collection.Count > 0)
                    {

                    }



                   else
                    {
                        conn.InsertAll(collection);

                    }
                }
                






                picker.ItemsSource = conn.Table<CategoryModel>().ToList();


                 

                    var Pickerlist = conn.Table<CategoryModel>().ToList();


                    List<CategoryModel> Finallist = new List<CategoryModel>();


                    foreach (var item in Pickerlist)
            {
                var exit = Finallist.Where(i => i.CategoryName == item.CategoryName).ToList();
                if (exit.Count == 0)
                {
                    Finallist.Add(item);
                }
            }

                 }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", "Product list is empty or an error occured!", "OK");
                Console.WriteLine( ex);
            }








        }

            public async Task<List<Products>> GetAllofTheProducts()
        {

            return (await firebase
              .Child("Persons")
              .OnceAsync<Products>()).Select(item => new Products
              {
                  ProductName = item.Object.ProductName,
                  
              }).ToList();
        }

        public async Task InsertProduct(int PID, string productName, string price, string url)
        {

            await firebase
              .Child("Products")
              .PostAsync(new Products() { ProductId = ProductId, RefUserID = PID, ProductName = productName, Price = price, URL = url});
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {

            // ProductId = Guid.NewGuid();

            await DisplayAlert("Alert", "Denna knapp ska tas bort!", "OK");


            //int seed = 1939;
            //ShortId.SetSeed(seed);
            //string newurl = "test";
            //int PID = 1;


            //await InsertProduct(PID, ProductName.Text, price.Text, newurl);
            //// txtId.Text = string.Empty;
            //// txtName.Text = string.Empty;
            //await DisplayAlert("Success", "Products Added Successfully", "OK");

            //var AllProducts = await GetAllofTheProducts();
            // lstPersons.ItemsSource = AllProducts;
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

        //Read     
        public static async Task<Products> GetProduct(string productName)
        {
            try
            {
                var allProducts = await GetAllProducts();
                await firebase
                .Child("Products")
                .OnceAsync<Products>();
                return allProducts.Where(a => a.ProductName == productName).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Insert a product    
        public static async Task<bool> AddProduct(int PID, string price, string productName, string ImgUrl)
        {
            try
            {

                await firebase
                .Child("Products")
                .PostAsync(new Products() { RefUserID = PID, Price = price, ProductName = productName, URL = ImgUrl});
                Console.WriteLine("produkten kunde läggas till utan problem");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Update     
        public static async Task<bool> UpdateProduct(string price, string productName)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Products")
                .OnceAsync<Products>()).Where(a => a.Object.ProductName == productName).FirstOrDefault();
                await firebase
                .Child("Products")
                .Child(toUpdateUser.Key)
                .PutAsync(new Products() { Price = price, ProductName = productName });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteProduct(string productName)
        {
            try
            {

                var toDeletePerson = (await firebase
                .Child("Products")
                .OnceAsync<Products>()).Where(a => a.Object.ProductName == productName).FirstOrDefault();
                await firebase.Child("Products").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task DeleteTestProduct(Guid productId)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Products>()).Where(a => a.Object.ProductId == productId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

            

        }

        public async Task DeleteTesting(Guid productId)
        {
            var toDeletePerson = (await firebase
                .Child("Persons")
                .OnceAsync<Products>()).FirstOrDefault(a => a.Object.ProductId == productId);
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();
        }



        /*private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            //await DeleteTestProduct(1);
            Console.WriteLine("Test av valet: "+SelectedPerson.ProductId);
            await DeleteTesting(SelectedPerson.ProductId);
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            var allPersons = await GetAllProducts();
            lstPersons.ItemsSource = allPersons;
        }*/




        //private async void BtnRetrive_Clicked(object sender, EventArgs e)
        //{
        //    var person = await firebaseHelper.GetPerson(Convert.ToInt32(txtId.Text));
        //    if (person != null)
        //    {
        //        txtId.Text = person.PersonId.ToString();
        //        txtName.Text = person.Name;
        //        await DisplayAlert("Success", "Person Retrive Successfully", "OK");

        //    }
        //    else
        //    {
        //        await DisplayAlert("Success", "No Person Available", "OK");
        //    }

        //}






        private async void btnPick_Clicked(object sender, EventArgs e)
            {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });
                if (file == null)
                    return;
                //imgChoosed.Source = ImageSource.FromStream(() => 
                //{
                //    var imageStram = file.GetStream();
                //    return imageStram;
                //});
                await StoreImages(file.GetStream());
                await DisplayAlert("Uploading", "Please wait 5 seconds, then press OK", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async void InsertIntoCloudDatabase_Clicked(object sender, EventArgs e)
        {
            try
            {
                


                var value = Application.Current.Properties["Username"].ToString();

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns
                    var data = conn.Table<User>().ToList();
                    var checkquery = conn.Table<User>().Where(a => a.Username == value).FirstOrDefault();

                    Console.WriteLine(value);






                    var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();


                    var Rows = new ObservableCollection<User>();
                    Rows.Clear();


                    foreach (var entry in useridcheck)
                    {

                        int pid = entry.UID;
                        Console.WriteLine("test av URL: " + entry.UID);



                        var length = entry.password.Length;






                        string Price = price.Text;
                        string productName = ProductName.Text;

                        Console.WriteLine("Första url visas här:" + Imgurl);

                        var products = new Products
                        {
                            ProductId = ProductId,
                            RefUserID = pid,
                            Price = Price,
                            ProductName = productName,
                            URL = Imgurl,
                        };

                        await InsertProduct(pid, ProductName.Text, price.Text, Imgurl);


                        //  var InsertFB = await AddProduct(pid, Price, productName, Imgurl);


                        conn.CreateTable<Products>();
                        int rowsAdded = conn.Insert(products);

                        await DisplayAlert("Congrats!", "A new product have been added!", "OK");
                        await Navigation.PushAsync(new ProductPage());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Alert", "Something went wrong!", "OK");
            }
        }



        public async Task<string> StoreImages(Stream imageStream)
        {
            var options = new GenerationOptions //Genererar ett unikt id med hjälp av Api:et shortid
            {
                UseNumbers = true,
                UseSpecialCharacters = false,
                Length = 9
            };

            string id = ShortId.Generate(options);
            
            //Lagrar bilden i molndatabas
            var storeImage = await new FirebaseStorage("sup2021-c58ec.appspot.com")
                    .Child("ProductImages")
                    .Child("image" + id + ".jpg")
                    .PutAsync(imageStream);

            Imgurl = storeImage;
            Console.WriteLine("Här visas url:" + Imgurl);

           
             return Imgurl;


       }
       /* private Products SelectedPerson => (Products)lstPersons.SelectedItem;*/


       

}
}