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
    public partial class MainMenu : TabbedPage
    {
        public MainMenu()
        {
            InitializeComponent();
           
            this.Children.Add(new ProductPage() { Title = "Products", IconImageSource = "navicon_products.png" });
            this.Children.Add(new Basket() { Title = "Basket" , IconImageSource = "navicon_shoppingcart.png" });
            this.Children.Add(new Wishlist() { Title = "Wishlist"  , IconImageSource = "navicon_wishlist.png" });
            this.Children.Add(new Profilepage() { Title = "Profile", IconImageSource = "navicon_profile.png" }); 
            this.Children.Add(new settings() { Title = "Settings" , IconImageSource = "navicon_settings.png" });

        }
    }
}