using SUP2021.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace SUP2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage :ContentPage
    {
        
        public LoginPage()
        {
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
           // Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            //var Appshell = new AppShell();

        }
    }
}