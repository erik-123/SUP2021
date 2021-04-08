using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using SUP2021.Views;

namespace SUP2021.ViewModels
{
    public class profileViewModel : BaseViewModel
    {
        public Command entryCommand { get; }
        public Command EditCommand { get; }

        public profileViewModel()
        {
            Title = "Profile";
            entryCommand = new Command(OnentryClicked);
            EditCommand = new Command(BtnOnEditClicked);

        }
        private async void BtnOnEditClicked (object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

        }
        private async void OnentryClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}
