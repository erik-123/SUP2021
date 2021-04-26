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
using SUP2021.Models;
using System.Net;
using SUP2021.ViewModels;

namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryTest : ContentPage
    {
       // private string getUrl = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreement";
        public CountryTest()
        {
            InitializeComponent();
            this.BindingContext = new ShippingViewModel();
        }
        public async void Postnordtest(object sender, EventArgs e)
        {
            //Api key: 03f0da2b63d3bf80b62eb88c4e1c40c8
            // new API key: 0ba91457361a67d1495aefa8519ba3cb
            //await DisplayAlert("","","");

            if ((string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtPostalCode.Text) || string.IsNullOrWhiteSpace(txtAdress.Text)
                 || string.IsNullOrEmpty(txtCity.Text) || string.IsNullOrEmpty(txtPostalCode.Text) || (string.IsNullOrEmpty(txtAdress.Text))))




            {
                await DisplayAlert("Alert", "A textfield can't be empty or lack value", "OK");
            }
            else
            {


                //Beräknar transportid
                //var client = new RestClient("https://atapi2.postnord.com/rest/transport/v1/transittime/getTransitTimeInformation.json?apikey=03f0da2b63d3bf80b62eb88c4e1c40c8&dateOfDeparture=2021-02-09&serviceCode=18&serviceGroupCode=SE&fromAddressStreetName=teststreet&fromAddressStreetNumber=1&fromAddressPostalCode=11759&fromAddressCountryCode=SE&toAddressStreetName=teststreet&toAddressStreetNumber=2&toAddressPostalCode=17173&toAddressCountryCode=SE&responseContent=simple");


                string city = txtCity.Text; //Working
                string postalCode = txtPostalCode.Text;
                string streetName = txtAdress.Text;
                //int streetNumber = 6;


                //Streetname: Fakultetsgatan
                //Streetnumber: 1 
                //Postalcode: 70281
                //City: Örebro


                string[] subs = streetName.Split(' ');

                foreach (var sub in subs)
                {
                    Console.WriteLine($"Substring: {sub}");


                }


                string x = subs[0];
                string y = subs[1];
                Console.WriteLine(city + postalCode + x + y);

                var body = new JsonRootAdressObject

                {
                    city = city,
                    postalCode = postalCode,
                    streetName = x,
                    streetNumber = y

                };





                // string url = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreement";

               // string url = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/bypostalcode?";

                string url2 = $"https://api2.postnord.com/rest/businesslocation/v5/servicepoints/bypostalcode?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&postalCode={postalCode}&context=optionalservicepoint&responseFilter=public&typeId=25&callback=jsonp";

                var client3 = new RestClient(url2);
                var request2 = new RestRequest(Method.GET);


               // request2.AddParameter("city", city, ParameterType.RequestBody);
                //request2.AddParameter("postalCode", postalCode, ParameterType.RequestBody);


               

                //request2.AddJsonBody(body);

               
                IRestResponse response = client3.Execute(request2);
                Console.WriteLine("Svaret kommer här"+ response.Content);

                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;


                await DisplayAlert("Vales from Api", response.Content, "OK", "Cancel");
                


                //var client = new RestClient("https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreementCountry=SE&city=Gislaved&postalCode=33234&streetName=Holmengatan&streetNumber=14&numberOfServicePoints=3&srId=EPSG:4326&context=optionalservicepoint&responseFilter=public&typeId=24,25,54&callback=jsonp");

                //var newrequest = new RestRequest("city={City}&postalCode={potalcode}&streetName=Holmengatan&streetNumber=14&numberOfServicePoints=3&srId=EPSG:4326&context=optionalservicepoint&responseFilter=public&typeId=24,25,54&callback=jsonp")
                //.AddUrlSegment("entity", "s2");



                //var client2 = new RestClient("https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreement");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //request.AddParameter("text/plain", "{\r\n  \"city\": {\r\n    \"service\": {\r\n      \"basicServiceCode\": \"19\",\r\n      \"additionalServiceCode\": [\r\n        \"A1\"\r\n      ]\r\n    },\r\n    \"items\": [\r\n      373500489530470000\r\n    ]\r\n  },\r\n  \"location\": {\r\n    \"place\": \"Company name or information about pickup place e.g. garage\",\r\n    \"streetName\": \"Skrindavägen\",\r\n    \"streetNumber\": \"117\",\r\n    \"postalCode\": 19162,\r\n    \"city\": \"Sollentuna\",\r\n    \"countryCode\": \"SE\"\r\n  },\r\n  \"order\": {\r\n    \"customerNumber\": \"1234567891\",\r\n    \"orderReference\": \"Ref-1212122A\",\r\n    \"contactName\": \"Nils Andersson\",\r\n    \"contactEmail\": \"Nils.Andersson@postnord.com\",\r\n    \"phoneNumber\": \"+4670788888\",\r\n    \"smsNumber\": \"+4670788888\",\r\n    \"entryCode\": \"8216\"\r\n  },\r\n  \"pickup\": [\r\n    {\r\n      \"typeOfItem\": \"parcel\",\r\n      \"noUnits\": 1\r\n    }\r\n  ],\r\n  \"dateAndTimes\": {\r\n    \"readyPickupDate\": \"2020-11-17T09:52:07.929Z\",\r\n    \"latestPickupDate\": \"2020-11-17T09:52:07.929Z\",\r\n    \"earliestPickupDate\": \"2020-11-17T09:52:07.929Z\"\r\n  }\r\n}", ParameterType.RequestBody);
                //IRestResponse response4 = client.Execute(request);
                //Console.WriteLine(response.Content);








                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                //await DisplayAlert("Vales from Api", response.Content, "OK", "Cancel");


                //var client = new RestClient("https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreementCountry=SE&city=Gislaved&postalCode=33234&streetName=Holmengatan&streetNumber=14&numberOfServicePoints=3&srId=EPSG:4326&context=optionalservicepoint&responseFilter=public&typeId=24,25,54&callback=jsonp");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                //await DisplayAlert("Vales from Api", response.Content, "OK", "Cancel");


            }
        }
        public async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaymentTest());
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