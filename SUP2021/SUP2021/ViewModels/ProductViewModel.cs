using SUP2021.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;
using Xamarin.CommunityToolkit.ObjectModel;
using SUP2021.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace SUP2021.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Command AddNewCommand { get; }
           
             

        public ProductViewModel()
        {
            AddNewCommand = new Command(OnAddNewClicked);

            Title = "Product";
        }



        private async void OnAddNewClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AddProductPage)}");
        }

    }

}

