using System;
using System.Collections.Generic;
using System.ComponentModel;

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
