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

namespace SUP2021.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Command AddNewCommand { get; }
        public Command LoadItemsCommand { get; }
        public ObservableRangeCollection<Products> AllItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }
        public ObservableRangeCollection<Products> collection { get; set; }

        string selectedFilter = "All";
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItems();

            }
        }


        public ProductViewModel()
        {
            AddNewCommand = new Command(OnAddNewClicked);

            Title = "Product";

            AllItems = new ObservableRangeCollection<Products>();

            ObservableRangeCollection<Products> collection = new ObservableRangeCollection<Products>();

            FilterOptions = new ObservableRangeCollection<string>
            {
             "All",
             "Kläder",
             "Elektronik",
             "Mat",
             "Annat" };

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        void FilterItems()
        {
            collection.ReplaceRange(AllItems.Where(s => s.ProductName == SelectedFilter || SelectedFilter == "All"));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))

                {
                    var products = conn.Table<Products>().ToList();
                    AllItems.ReplaceRange(products);
                    FilterItems();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }




        private async void OnAddNewClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AddProductPage)}");
        }

    }

}

