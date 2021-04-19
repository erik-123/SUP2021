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
    public partial class CountryTest : ContentPage
    {
        public CountryTest()
        {
            InitializeComponent();
        }
        public async void Postnordtest(object sender, EventArgs e) 
        {
            //Api key: 03f0da2b63d3bf80b62eb88c4e1c40c8
            await DisplayAlert("","","");



        }



    }
}