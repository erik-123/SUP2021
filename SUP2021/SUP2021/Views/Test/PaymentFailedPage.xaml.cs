using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentFailedPage : ContentPage
    {
        public Guid PaymentFailID { get; set; }

        public DateTime FailDate { get; set; }
        public PaymentFailedPage()
        {
            InitializeComponent();
            this.PaymentFailID = Guid.NewGuid();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            lblPaymentID.Text = PaymentFailID.ToString();
            DateTime now = DateTime.Now;
            lblDate.Text = now.ToString();


        }
        public async void OnRetryClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ProductPage)}");

        }

        public async void OnFAQClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(FAQ)}");

        }
    }
}