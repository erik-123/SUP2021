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
using System.Text.RegularExpressions;
using SQLite;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace SUP2021.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryTest : ContentPage
    {
        // private string getUrl = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreement";

        public IList<Service> pickup { get; set; }
        public CountryTest()
        {
            InitializeComponent();
            // this.BindingContext = new ShippingViewModel();
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();

            checkboxstandard.IsEnabled = false;

        }

        public async void Postnordtest(object sender, EventArgs e)
        {
            //Api key: 03f0da2b63d3bf80b62eb88c4e1c40c8
            // new API key: 0ba91457361a67d1495aefa8519ba3cb
            //await DisplayAlert("","","");

            //if ((string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtPostalCode.Text) || string.IsNullOrWhiteSpace(txtAdress.Text)
            //     || string.IsNullOrEmpty(txtCity.Text) || string.IsNullOrEmpty(txtPostalCode.Text) || (string.IsNullOrEmpty(txtAdress.Text))))




            //{
            //    await DisplayAlert("Alert", "A textfield can't be empty or lack value", "OK");
            //}
            //else
            //{

            if ((string.IsNullOrWhiteSpace(txtPostalCode.Text) || string.IsNullOrEmpty(txtPostalCode.Text)))

            {
                await DisplayAlert("Alert", "Postcode needs to have a value!", "OK");
            }
            else
            {




                //Beräknar transportid
                //var client = new RestClient("https://atapi2.postnord.com/rest/transport/v1/transittime/getTransitTimeInformation.json?apikey=03f0da2b63d3bf80b62eb88c4e1c40c8&dateOfDeparture=2021-02-09&serviceCode=18&serviceGroupCode=SE&fromAddressStreetName=teststreet&fromAddressStreetNumber=1&fromAddressPostalCode=11759&fromAddressCountryCode=SE&toAddressStreetName=teststreet&toAddressStreetNumber=2&toAddressPostalCode=17173&toAddressCountryCode=SE&responseContent=simple");


                //string city = txtCity.Text; 
                string postalCode = txtPostalCode.Text;
                //string streetName = txtAdress.Text;
                //int streetNumber = 6;


                //Streetname: Fakultetsgatan
                //Streetnumber: 1 
                //Postalcode: 70281
                //City: Örebro


                //try
                //{
                //    string[] subs = streetName.Split(' ');

                //    foreach (var sub in subs)
                //    {
                //        Console.WriteLine($"Substring: {sub}");


                //    }


                //    string x = subs[0];
                //    string y = subs[1];
                //    Console.WriteLine(city + postalCode + x + y);


                //    var body = new JsonRootAdressObject

                //    {
                //        city = city,
                //        postalCode = postalCode,
                //        streetName = x,
                //        streetNumber = y

                //    };

                //}
                //catch (Exception ex)

                //{
                //    Console.WriteLine(ex);
                //    await DisplayAlert("Alert!", "Incorrect adress, try again", "OK");

                //}






                // string url = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/nearest/byaddress?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&agreement";

                // string url = "https://api2.postnord.com/rest/businesslocation/v5/servicepoints/bypostalcode?";

                string url2 = $"https://api2.postnord.com/rest/businesslocation/v5/servicepoints/bypostalcode?apikey=0ba91457361a67d1495aefa8519ba3cb&returnType=json&countryCode=SE&postalCode={postalCode}&context=optionalservicepoint&responseFilter=public&typeId=25&callback=jsonp";

                var client3 = new RestClient(url2);
                var request2 = new RestRequest(Method.GET);

                //var deserilzaer = new RestSharp.Deserializers.DotNetXmlDeserializer(); 


                // request2.AddParameter("city", city, ParameterType.RequestBody);
                //request2.AddParameter("postalCode", postalCode, ParameterType.RequestBody);

                // IRestResponse<List<Models.ServicePointNames>> response4 = client3.Get<List<Models.ServicePointNames>>(request2); //Ny


                //request2.AddJsonBody(body);


                IRestResponse response = client3.Execute(request2);
                // Console.WriteLine("Svaret kommer här"+ response4.Content);

                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;

                if (response.IsSuccessful)
                {


                    Console.WriteLine(numericStatusCode);
                    //Models.ServicePointNames data = deserilzaer.Deserialize<Models.ServicePointNames>(response4);
                    // Console.WriteLine(data.name);


                    await DisplayAlert("Vales from Api", response.Content, "OK", "Cancel");

                }
                else {

                    await DisplayActionSheet("Error", "Something went wrong", "OK", "Cancel");
                    Console.WriteLine(numericStatusCode);
                }







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
        private async void BookNewOrder()
        {
            try
            {

                var streetaddress = txtAdress.Text;
                var postalcode = txtPostalCode.Text;
                int convertedpostalcode = int.Parse(postalcode);
                var city = txtCity.Text;

                string[] subs = streetaddress.Split(' ');

                foreach (var sub in subs)
                {
                    Console.WriteLine($"Substring: {sub}");


                }


                string streetname = subs[0];
                string streetNumber = subs[1];             


                var additionalServiceCodeStandard = new List<string>
            {
                "A1"
            };
                var itemList = new List<long>
            {
                373500489530470000
            };

             

                List<PickupService> newist = new List<PickupService>();

                newist.Add(new PickupService() {  typeOfItem = "parcel",  noUnits = 1 });




                Service Servicebody = new Service
                {
                    
                    basicServiceCode = "19",
                    additionalServiceCode = new List<string>
            {
                "A1"
            }

            };

                

                Service2 service2 = new Service2
                {
                    service = Servicebody

                };
                
                Shipment2 shipment2 = new Shipment2
                {
                    shipment = service2
                };

                var Shipmentbody = new Shipment
                {
                     
                    items = itemList

                };
                var LocationBody = new Location
                {
                    place = "Company name or information about pickup place e.g.garage",
                    streetName = streetname,
                    streetNumber = streetNumber,
                    postalCode = convertedpostalcode,
                    city = city,
                    countryCode = "SE"


                };

                Location2 location2 = new Location2
                {
                     location = LocationBody
                };

                var OrderBody = new Order
                {
                    customerNumber = "1234567891",
                    orderReference = "Ref-1212122A",
                    contactName = "Nils Andersson",
                    contactEmail = "Nils.Andersson@postnord.com",
                    phoneNumber = "+4670788888",
                    smsNumber = "+4670788888",
                    entryCode = "8216"
                };

                var Order2 = new Order2
                { 
                     order = OrderBody
                
                };

                var PickUpBody = new PickupService
                {
                    
                    typeOfItem = "parcel",
                    noUnits = 1

                };

                var PickUp2 = new PickUp2
                {
                    
                     pickups = newist

                };


                var datesandtimesBody = new DateAndTimes
                {
                    readyPickupDate = new DateTime(2021, 5, 13),
                    latestPickupDate = new DateTime(2021, 5, 13),
                    earliestPickupDate = new DateTime(2021, 5, 13)

            };

                var datesandtimes2 = new DateAndTimes2
                {
                    DateAndTimes  = datesandtimesBody

                };

               

                var client = new RestClient("https://api2.postnord.com/rest/order/v1/pickup/SE?apikey=0ba91457361a67d1495aefa8519ba3cb");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                // request.AddParameter("text/plain", "{\r\n  \"shipment\": {\r\n    \"service\": {\r\n      \"basicServiceCode\": \"19\",\r\n      \"additionalServiceCode\": [\r\n        \"A1\"\r\n      ]\r\n    },\r\n    \"items\": [\r\n      373500489530470000\r\n    ]\r\n  },\r\n  \"location\": {\r\n    \"place\": \"Company name or information about pickup place e.g. garage\",\r\n    \"streetName\": \"streetname\",\r\n    \"streetNumber\": \"streetNumber\",\r\n    \"postalCode\": postalcode,\r\n    \"city\": \"txtCity\",\r\n    \"countryCode\": \"SE\"\r\n  },\r\n  \"order\": {\r\n    \"customerNumber\": \"1234567891\",\r\n    \"orderReference\": \"Ref-1212122A\",\r\n    \"contactName\": \"Nils Andersson\",\r\n    \"contactEmail\": \"Nils.Andersson@postnord.com\",\r\n    \"phoneNumber\": \"+4670788888\",\r\n    \"smsNumber\": \"+4670788888\",\r\n    \"entryCode\": \"8216\"\r\n  },\r\n  \"pickup\": [\r\n    {\r\n      \"typeOfItem\": \"parcel\",\r\n      \"noUnits\": 1\r\n    }\r\n  ],\r\n  \"dateAndTimes\": {\r\n    \"readyPickupDate\": \"2020-11-17T09:52:07.929Z\",\r\n    \"latestPickupDate\": \"2020-11-17T09:52:07.929Z\",\r\n    \"earliestPickupDate\": \"2020-11-17T09:52:07.929Z\"\r\n  }\r\n}", ParameterType.RequestBody);

                
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();

                 

                string json = JsonConvert.SerializeObject(shipment2, Formatting.Indented);
                string json2 = JsonConvert.SerializeObject(Shipmentbody, Formatting.Indented);
                string json3 = JsonConvert.SerializeObject(location2, Formatting.Indented);
                string json4 = JsonConvert.SerializeObject(Order2, Formatting.Indented);
                string json5 = JsonConvert.SerializeObject(PickUp2, Formatting.Indented);
                string json6 = JsonConvert.SerializeObject(datesandtimes2, Formatting.Indented);

                await DisplayAlert("Alert", json+json2+json3+json4+json5+json6, "OK");
                Console.WriteLine(json + json2 + json3 + json4 + json5 + json6);              

                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(shipment2);
                request.AddJsonBody(Shipmentbody);
                request.AddJsonBody(location2);
                request.AddJsonBody(Order2);               
                request.AddJsonBody(PickUp2);                
                request.AddJsonBody(datesandtimes2);              

                IRestResponse response = client.Execute(request);
                await DisplayAlert("Alert", response.Content.ToString(), "OK");

            }
            catch (Exception ex)

            {
                Console.WriteLine(ex);
                await DisplayAlert("Alert!", "Incorrect adress, try again", "OK");

            } 
        
        }
    

        public async void OnNextButtonClicked(object sender, EventArgs e)
        {

            //lblAddress
            //lblPostalCode
            //lblCity

            if ((string.IsNullOrWhiteSpace(txtAdress.Text) || string.IsNullOrWhiteSpace(txtPostalCode.Text) || string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrEmpty(txtAdress.Text) || string.IsNullOrEmpty(txtPostalCode.Text) || string.IsNullOrEmpty(txtCity.Text)))

            {
                await DisplayAlert("Alert", "A textfield can't be empty or lack value", "OK");              

            }
            else
            {               //(txtAdress.Text, @"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)       

                string input = txtCity.Text;
                bool isDigitPresent = input.Any(c => char.IsDigit(c));

                if (!Regex.Match(txtAdress.Text, @"(^[a-zA-Z]+\s+\d+[0-9]$)$").Success)
                {

                    await DisplayAlert("Alert", "Address has the wrong format! Addresses need to have the following format: street 124 ", "OK");
                }
                else if (txtPostalCode.Text.Length < 5)
                {
                    await DisplayAlert("Alert", "Postcodes need to have a 5 digit number!", "OK");

                }
                else if (isDigitPresent) 
                {
                    await DisplayAlert("Alert", "City cannot contain numbers!", "OK");
                }
                else
                {
                    
                    var value = Application.Current.Properties["Username"].ToString();

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {

                        conn.CreateTable<User>();
                        var data = conn.Table<User>().ToList();
                        var checkquery = conn.Table<User>().Where(a => a.Username == value).FirstOrDefault();

                        Console.WriteLine(value);






                        var useridcheck = conn.Table<User>().Where(c => c.Username == value).ToList();



                        var Rows = new ObservableCollection<User>();
                        Rows.Clear();


                        foreach (var entry in useridcheck)
                        {

                            int pid = entry.UID;                       

                            var length = entry.password.Length;


                            string city = txtCity.Text;
                            string postalCode = txtPostalCode.Text;
                            string streetName = txtAdress.Text;



                            var shippingInfo = new ShippingModel
                            {
                                ShippingId = Guid.NewGuid(),
                                Address = streetName,
                                City = city,
                                Postalcode = postalCode,
                                UID = pid,
                               
                            };




                            conn.CreateTable<ShippingModel>();
                            int rowsAdded = conn.Insert(shippingInfo);

                            await DisplayAlert("Congrats!", "A new product have been added!", "OK");

                            await Navigation.PushAsync(new PaymentTest());


                        }
                    } 
                }
            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync($"//{nameof(Basket)}");
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