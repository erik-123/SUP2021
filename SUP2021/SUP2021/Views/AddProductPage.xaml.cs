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


namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        MediaFile file;

        private string imgurl { get; set; }



        public AddProductPage()
        {
            InitializeComponent();


            //standard bilder om ingen bild valts
            //imgBanner.Source = ImageSource.FromResource("XamarinFirebase.images.banner.png");
            //imgChoosed.Source = ImageSource.FromResource("XamarinFirebase.images.default.jpg");


        }

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
            int pid = 1;
            string Price = price.Text;
            string Name = ProductName.Text;

            Console.WriteLine("Första url visas här:" + imgurl);

            var products = new Products
            {
                PID = pid,
                Price = Price,
                ProductName = Name,
                URL = imgurl,
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Products>();
                int rowsAdded = conn.Insert(products);

                await DisplayAlert("Congrats!", "A new product have been added!", "OK");
                await Navigation.PushAsync(new ProductPage());
            }
        }


        public async Task<string> StoreImages(Stream imageStream)
        {
            var options = new GenerationOptions //Genererar ett unikt bild med hjälp av Api:et shortid
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

            imgurl = storeImage;
            Console.WriteLine("Här visas url:" + imgurl);

           
             return imgurl;


       }
    }
}