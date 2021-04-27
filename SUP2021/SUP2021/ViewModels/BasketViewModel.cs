using SUP2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SUP2021.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        public ObservableCollection<Products> Products { get; set; }
        public Command<Products> RemoveCommand 
        {
            get 
            {
                return new Command<Products>((product)=>
                {
                    Products.Remove(product);
                }
                
                );
            
            }

        
        }


        public BasketViewModel()
        {
            Title = "Shoppingcart";
        }
    }
}