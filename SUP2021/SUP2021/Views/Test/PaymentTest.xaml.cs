using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentTest : ContentPage
    {
        public PaymentTest()
        {
            InitializeComponent();
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

            await DisplayAlert("Alert","Skriv inte in ditt eget kort","OK");



        }


            public async void PayViaStripe()
            {
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

                // Skapar testkund
                CustomerCreateOptions customer = new CustomerCreateOptions
                {
                    Name = "Test",
                    Email = "test@gmail.com",
                    Description = "Paying 5 sek",
                    Address = new AddressOptions { City = "Sthlm", Country = "Sweden", Line1 = "Sample Address", Line2 = "Sample Address 2", PostalCode = "10030" }
                };

                var customerService = new CustomerService();
                var cust = customerService.Create(customer);

                // step 5: charge option
                var chargeoption = new ChargeCreateOptions
                {
                    Amount = 1500, //Måste vara högre än 3,00 dvs 300
                    Currency = "SEK",
                    ReceiptEmail = "test@gmail.com",
                    Customer = cust.Id,
                    Source = source.Id
                };

                // step 6: charge the customer
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(chargeoption);
                if (charge.Status == "succeeded")
                {
                    Console.WriteLine("Success");
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
        }
    }
