using SUP2021.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SUP2021.ViewModels
{
   public class ProductViewModel
    {
        public Command AddNewCommand { get; }
        


        public ProductViewModel()
        {
            AddNewCommand = new Command(OnAddNewClicked);
            

        }

        private async void OnAddNewClicked(object obj)
        {
             // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AddProductPage)}");
        }       

    }
}
