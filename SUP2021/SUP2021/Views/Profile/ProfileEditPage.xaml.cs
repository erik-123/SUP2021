using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using shortid;
using shortid.Configuration;
using SQLite;
using SUP2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : ContentPage
    { 
        MediaFile file;
        private string Imgurl { get; set; }

        public static FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/");

        public ProfileEditPage()
        {
            InitializeComponent();
        }

        private async void BtnPickImage_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                
               await StoreImages(file.GetStream());
               await DisplayAlert("Uploading", "Please wait 5 seconds, then press OK", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InsertUser(int UserID, string adress, string Firstname, string surname, string username, string password, string number, string email, string postcode, string URL)
        {
                       
            await firebase
              .Child("Users")
              .PostAsync(new User() { UID = UserID, adress = adress, Username = username, firstname = Firstname, sername = surname, password = password, nummber = number,  email= email, postcode=postcode, URL = URL });

            }
           




    


        private async void InsertIntoDatabase_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = Firstname.Text;
                string user = username.Text;
                string Surname = surname.Text;
                string Email = email.Text;
                string Password = password.Text;
                string Phone = phone.Text;
                string Adress = adress.Text;
                string Postcode = postcode.Text;
                int uid = 1;


                Console.WriteLine("Första url visas här:" + Imgurl);

                var updateduser = new User
                {
                    URL = Imgurl,
                    UID = uid,
                    firstname = name,
                    Username = user,
                    sername = Surname,
                    email = Email,
                    password = Password,
                    nummber = Phone,
                    adress = Adress,
                    postcode = Postcode

                };



               await InsertUser(uid, name, user, Surname, Email, Password, Phone, Adress, Postcode, Imgurl);



                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>();
                    int rowsAdded = conn.Update(updateduser);

                    await DisplayAlert("Congrats!", "User have been updated", "OK");
                    await Navigation.PushAsync(new Profilepage());
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
                    .Child("ProfileImages")
                    .Child("image" + id + ".jpg")
                    .PutAsync(imageStream);

            Imgurl = storeImage;
            Console.WriteLine("Här visas url:" + Imgurl);


            return Imgurl;


        }




    }
}