using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SQLite;
using SUP2021.Models;
using SUP2021.Services;
using SUP2021.ViewModels;
using SUP2021.Views.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SUP2021.Views
{
    public partial class settings : ContentPage
    {
        public settings()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
        }

        public async void OnEditProfilebuttonclicked(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new ProfileEditPage());
        }

        public async void OnEditPermissionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPermissions());

        }
        public async void BtnSend_Clicked(object sender, System.EventArgs e)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();

                var products = conn.Table<User>().ToList();
                foreach (var a in products)
                {
                    List<string> toAddress = new List<string>();
                    var email = a.email;
                    toAddress.Add(email);

                    toAddress.Add("projektbloggsup@gmail.com");

                    string subject = "Rabatt";
                    string body = "har du hört...";
                    await SendEmail(subject, body, toAddress);
                }
            }
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                Console.WriteLine(fbsEx);
                await DisplayAlert("Alert!", "Saknar mailfunktion på din enhet! Var god att logga in via webbläsare istället!", "OK");

                await Launcher.CanOpenAsync("https://accounts.google.com/signin/v2/identifier?passive=1209600&continue=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&followup=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
                
                await Launcher.OpenAsync("https://accounts.google.com/signin/v2/identifier?passive=1209600&continue=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&followup=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
                
               
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                Console.WriteLine(ex);
                await DisplayAlert("Alert!","Something went wrong!","OK");
            }
        }





        public async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            

            //bool value = e.Value;
            //var permissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            //if (value == true)
            //{

            //    if (permissions != PermissionStatus.Granted)
            //    {
            //        permissions = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            //        //Console.WriteLine("ingenting behövs att göra");

            //    }
            //    if (permissions != PermissionStatus.Granted)
            //    {
            //        Console.WriteLine("tillstånd nekas");
            //        return;

            //    }
            //}
            //else {

            //    if (permissions != PermissionStatus.Denied)
            //    {
            //        Console.WriteLine("tillstånd nekas");

            //        return;

            //    }

            //    Console.WriteLine("Värdet är falskt"+value);
            //}



        }

    }
}
