using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

       async void Button_ClickedFacebook(System.Object sender, System.EventArgs e)
        {
            if (await Launcher.CanOpenAsync("https://m.facebook.com"))
            {
                await Launcher.OpenAsync("https://m.facebook.com");
            }
        }

        async void Button_ClickedInstagram(System.Object sender, System.EventArgs e)
        {
            if (await Launcher.CanOpenAsync("https://www.instagram.com"))
            {
                await Launcher.OpenAsync("https://www.instagram.com");
            }
        }

        async void Button_ClickedTwitter(System.Object sender, System.EventArgs e)
        {
            if (await Launcher.CanOpenAsync("https://mobile.twitter.com/?lang=en"))
            {
                await Launcher.OpenAsync("https://mobile.twitter.com/?lang=en");
            }
        }

        async void Button_ClickedLinkedIn(System.Object sender, System.EventArgs e)
        {
            if (await Launcher.CanOpenAsync("https://se.linkedin.com"))
            {
                await Launcher.OpenAsync("https://se.linkedin.com");
            }
        }
    }

}