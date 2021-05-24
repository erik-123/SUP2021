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
    public partial class PaymentSuccesfulPage : ContentPage
    {

        private int PriceSum { get; set; }
        private Guid TransactionID { get; set; }
        public PaymentSuccesfulPage()
        {
            InitializeComponent();

            this.BindingContext = this;
            this.TransactionID = Guid.NewGuid();

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            var value = Application.Current.Properties["SUM"].ToString();
            PriceSum = int.Parse(value);
            lblsum.Text = PriceSum.ToString();
            lblTID.Text = TransactionID.ToString();

            try
            {
                var username = Application.Current.Properties["Username"].ToString();
               
                
                    

                        

                        

                       
                      


                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<User>();
                  
                    var useridcheck = conn.Table<User>().Where(c => c.Username == username).ToList();
                    

                   foreach (var b in useridcheck)
                    {
                        var UID = b.UID;
                        lblMobile.Text = b.nummber;                       


                        }




                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Alert", "Something went wrong!", "OK");


            }

        }

        public async void OnButtonClosedClicked(Object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ProductPage());
            await Shell.Current.GoToAsync($"//{nameof(ProductPage)}");


        }      


    }
}