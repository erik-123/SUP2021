using System;
using System.Collections.Generic;
using SUP2021.ViewModels;
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
    }
}
