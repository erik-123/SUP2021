using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Shiny;
using Shiny.Notifications;
using SQLite;
using Stripe;
using SUP2021.Models;
using SUP2021.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentTest : ContentPage
    {
        private string Firstname { get; set; }
        private string Surname { get; set; }
        private string Fullname { get; set; }

        private string CityName { get; set; }
        private string PostalCode { get; set; }
        private string StreetName { get; set; }

        private int ResultCurrency { get; set; }
        private int Price { get; set; }


        public PaymentTest()
        {
            InitializeComponent();
            this.BindingContext = new PaymentViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Omvandlar textens färg från Rosa (standard) till Svart
            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.Black);
            template.SetBinding(TextCell.TextProperty, ".");
            testcreditcardslist.ItemTemplate = template;
            testscenariocreditcardslist.ItemTemplate = template;
            await SendNotificationNow();
            await ScheduleLocalNotification(DateTimeOffset.UtcNow.AddMinutes(1));

            await DisplayAlert("Alert", "Skriv inte in ditt eget kort", "OK");



        }

        Task SendNotificationNow()
        {
            var notification = new Notification
            {
                Title = "Har du fått med dig allt?",
                Message = "",
            };

            return ShinyHost.Resolve<INotificationManager>().RequestAccessAndSend(notification);
        }

        Task ScheduleLocalNotification(DateTimeOffset scheduledTime)
        {
            var notification = new Notification
            {
                Title = "Tack för att du handlar hos oss!",
                Message = "",
                ScheduleDate = scheduledTime
            };

            return ShinyHost.Resolve<INotificationManager>().Send(notification);
        }



        async void OnOpenSwishButton_Clicked(System.Object sender, System.EventArgs e)
        {

            GetUserInfo();
            await DisplayAlert("Alert", Fullname, "OK");

            //if (await Launcher.CanOpenAsync("swish://"))
            //{
            //    Console.WriteLine("true");
            //    await Launcher.OpenAsync("swish://");
            //}
            //else {

            //    Console.WriteLine("Något gick fel");
            //}
        }



        public async void PayViaStripe()
        {
            //GetUserInfo();

            try
            {
                //Hemlig nyckel bör inte lagras här
                StripeConfiguration.ApiKey = "sk_test_51Iegu7KmvdxedTl5ip7GihXrL1wRf2MR2ATxllYpffbDUkiRuDQre1zoIbUoRL1qPFovl0UOwjNGUpMd259Jp3UQ00Q6X3y5Mi";

                string cardno = cardNo.Text;
                string expMonth = expireMonth.Text;
                string expYear = expireYear.Text;
                string cardCvv = cvv.Text;

                // TokenCardOptions

                TokenCardOptions stripeOption = new TokenCardOptions();
                stripeOption.Number = cardno;
                stripeOption.ExpYear = Convert.ToInt64(expYear);
                stripeOption.ExpMonth = Convert.ToInt64(expMonth);
                stripeOption.Cvc = cardCvv;

                // Assign card to token object
                TokenCreateOptions stripeCard = new TokenCreateOptions();
                stripeCard.Card = stripeOption;

                TokenService service = new TokenService();
                Token newToken = service.Create(stripeCard);

                // Assigning the token to the source
                var option = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "sek",
                    Token = newToken.Id
                };

                var sourceService = new SourceService();
                Source source = sourceService.Create(option);



                TestSum();
                GetUserInfo();
                GetShippingInfo();
                // Skapar testkund
                CustomerCreateOptions customer = new CustomerCreateOptions
                {
                    Name = Fullname,
                    Email = "projektbloggsup@gmail.com", //test email can be changed
                    Description = "Paying " + Price + " sek",
                    // Address = new AddressOptions { City = "Sthlm", Country = "Sweden", Line1 = "Sample Address", Line2 = "Sample Address 2", PostalCode = "10030" }
                    Address = new AddressOptions { City = CityName, Country = "Sweden", Line1 = StreetName, Line2 = "Sample Address 2", PostalCode = PostalCode }
                };

                var customerService = new CustomerService();
                var cust = customerService.Create(customer);



                // Charge option
                var chargeoption = new ChargeCreateOptions
                {
                    Amount = ResultCurrency, //Måste vara högre än 3,00 dvs 300 //3000=30
                    Currency = "SEK",
                    ReceiptEmail = "projektbloggsup@gmail.com",
                    Customer = cust.Id,
                    Source = source.Id
                };

                // Charge the customer
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(chargeoption);
                if (charge.Status == "succeeded")
                {
                    Console.WriteLine("Success");
                    RemoveFromShoppingCart();
                    await Navigation.PushAsync(new PaymentSuccesfulPage());

                }
                else
                {
                    Console.WriteLine("Payment failed");

                }
            }
            catch (Exception ex) {

                await DisplayAlert("Alert", ex.ToString(), "OK");
                Console.WriteLine(ex);
            }
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            PayViaStripe();
        }




        private async void GetUserInfo()
        {
            var value = Application.Current.Properties["Username"].ToString();
            Console.WriteLine(value);
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    conn.CreateTable<User>(); //SQLite ignorerar create table om den redan finns              
                                              //var getuserquery = conn.Table<User>().Where(a => a.Username == value).FirstOrDefault();

                    var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();

                    var Rows = new ObservableCollection<User>();
                    Rows.Clear();
                    Console.WriteLine(useridcheck);







                    foreach (var a in useridcheck)

                    {



                        Console.WriteLine(a.UID + a.firstname + a.sername);

                        //a.Username = Firstname;
                        //a.sername = Surname;
                        Firstname = a.firstname;
                        a.firstname = Firstname;
                        Console.WriteLine("Här skrivs förnamnet ut " + Firstname);

                        Surname = a.sername;
                        Console.WriteLine("Här skrivs efternamnet ut " + Surname);
                        Fullname = String.Concat(Firstname, " ", Surname);

                        //Console.WriteLine(Surname);
                        //Console.WriteLine(a.sername);

                        //Fullname = String.Concat(Firstname, Surname);


                    }



                    Console.WriteLine(Fullname);






                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Something went wrong!", "OK");

                Console.WriteLine("testmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("testmeddelande");



            }



        }

        private async void GetShippingInfo()
        {
            var value = Application.Current.Properties["Username"].ToString();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ShippingModel>();
                    conn.CreateTable<User>();
                    var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();




                    var Rows = new ObservableCollection<User>();
                    Rows.Clear();
                    Console.WriteLine(useridcheck);



                    foreach (var a in useridcheck)
                    {
                        var shippingModels = conn.Table<ShippingModel>().Where(c => c.UID == a.UID).ToList();

                        foreach (var b in shippingModels)
                        {

                            Console.WriteLine(a.UID + a.firstname + a.sername);


                            CityName = b.City;
                            PostalCode = b.Postalcode;
                            StreetName = b.Address;


                        }

                    }

                }
            }


            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Something went wrong!", "OK");

                Console.WriteLine("testmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("testmeddelande");



            }
        }

        private async void RemoveFromShoppingCart()
        {

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.DropTable<ShoppingCartModel>();
                    var task = new FirebaseStorage("sup2021-c58ec.appspot.com")
                    .Child("ProductImages").DeleteAsync();

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Error occured when buying the product!", "OK");
                Console.WriteLine("Felmeddelande");
                Console.WriteLine(ex);
                Console.WriteLine("Felmeddelande");

            }

        }  
        
        

        private void TestSum()
        {
           
            var value = Application.Current.Properties["SUM"].ToString();
            Price = int.Parse(value);
            
            int currency = 100; //convert to Swedish Currency

            ResultCurrency = Price * currency;


           
        }

    }
}
    
    
    






