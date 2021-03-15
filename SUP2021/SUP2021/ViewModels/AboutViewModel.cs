using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using SUP2021.Views;

namespace SUP2021.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command entryCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
           entryCommand = new Command(OnentryClicked);

        }
        private async void OnentryClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        
    }
}