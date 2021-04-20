using SUP2021.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp;

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
            // new API key: 0ba91457361a67d1495aefa8519ba3cb
            //await DisplayAlert("","","");




            //Beräknar transportid
            //var client = new RestClient("https://atapi2.postnord.com/rest/transport/v1/transittime/getTransitTimeInformation.json?apikey=03f0da2b63d3bf80b62eb88c4e1c40c8&dateOfDeparture=2021-02-09&serviceCode=18&serviceGroupCode=SE&fromAddressStreetName=teststreet&fromAddressStreetNumber=1&fromAddressPostalCode=11759&fromAddressCountryCode=SE&toAddressStreetName=teststreet&toAddressStreetNumber=2&toAddressPostalCode=17173&toAddressCountryCode=SE&responseContent=simple");


            string city = txtCity.Text; //Working
            string postalCode = txtPostalCode.Text;
            string streetName = txtAdress.Text;
            int streetNumber = 6;


            string[] subs = streetName.Split(' ');

            foreach (var sub in subs)
            {
                Console.WriteLine($"Substring: {sub}");
            }



            Console.WriteLine(city + postalCode + streetName + streetNumber);


            //var client = new RestClient("https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreementCountry=SE&city=Gislaved&postalCode=33234&streetName=Holmengatan&streetNumber=14&numberOfServicePoints=3&srId=EPSG:4326&context=optionalservicepoint&responseFilter=public&typeId=24,25,54&callback=jsonp");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //await DisplayAlert("Vales from Api", response.Content, "OK", "Cancel");



        }


        public class PostObject
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }




    }
}