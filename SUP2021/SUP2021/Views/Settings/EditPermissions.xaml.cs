using SUP2021.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPermissions : ContentPage
    {
        public EditPermissions()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
        }
    }
}