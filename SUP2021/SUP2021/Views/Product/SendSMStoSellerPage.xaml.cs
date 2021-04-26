using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendSMStoSellerPage : ContentPage
    {
        public SendSMStoSellerPage()
        {
            InitializeComponent();
        }
    
        private async void SMSButtonClicked(Object sender, EventArgs e)
        {
            try
            {

                await Sms.ComposeAsync(new SmsMessage
                {
                    Body = EntryMessage.Text,
                    Recipients = new List<string> { EntryNumber.Text }


                });

            }
            catch (Exception)
            
            {
                Console.WriteLine("Device is missing sms-client");
               
            
            }

        }
    }
}