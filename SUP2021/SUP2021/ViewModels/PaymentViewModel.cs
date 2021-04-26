using SUP2021.Models;
using SUP2021.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using SUP2021.Views.Test;

namespace SUP2021.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        #region Fields

        private CountryModel _selectedCountry;

        #endregion Fields


        #region Constructors

        public PaymentViewModel()
        {
            Title = "Payment";
            SelectedCountry = CountryUtils.GetCountryModelByName("Sweden");
            ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
            CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as CountryModel));
        }

        #endregion Constructors

        #region Properties

        public CountryModel SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        #endregion Properties

        #region Commands

        public ICommand ShowPopupCommand { get; }
        public ICommand CountrySelectedCommand { get; }

        #endregion Commands


        #region Private Methods

        private Task ExecuteShowPopupCommand()
        {
            var popup = new CountryPopupPage(SelectedCountry)
            {
                CountrySelectedCommand = CountrySelectedCommand
            };
            return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
        }

        private void ExecuteCountrySelectedCommand(CountryModel country)
        {
            SelectedCountry = country;
        }

        #endregion Private Methods
    }
}

