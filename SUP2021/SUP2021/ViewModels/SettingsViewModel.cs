using System;
using SUP2021.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace SUP2021.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public ICommand OnSelectPermissionChangeCommand { get; set; }
        public ICommand GoHomeCommand { get; set; }
        public ICommand LoadPermissionCommand { get; set; }
        public List<Settings> PermissionsList { get; set; }
        public Settings PermissionSelected { get; set; }

        public SettingsViewModel()
        {
            Title = "Settings";

            LoadPermissionCommand = new Command(async () => await LoadPermissions());
            LoadPermissionCommand.Execute(null);

            OnSelectPermissionChangeCommand = new Command(async () =>
            {
                if (PermissionSelected != null)
                {
                    PermissionSelected.IsGranted = await CheckAndRequestPermissionAsync(PermissionSelected.Permission) == PermissionStatus.Granted;
                }
            });

            GoHomeCommand = new Command(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Hey", "Welcome to my App", "Ok");
            });
        }

        async Task LoadPermissions()
        {
            PermissionsList = new List<Settings>()
            {
                { await CreatePermission(new Camera(), "ic_cam", "Camera", "So your friends can see you")},
                { await CreatePermission(new Microphone(), "ic_mic", "Mic", "So your friends can hear you") },
                { await CreatePermission(new LocationWhenInUse(), "ic_location", "Location", "So your friends can find you") }
            };
        }

        async Task<Settings> CreatePermission(BasePermission permission, string icon, string name, string description)
        {
            return new Settings()
            {
                Icon = icon,
                Permission = permission,
                Name = name,
                Description = description,
                IsGranted = await permission.CheckStatusAsync() == PermissionStatus.Granted
            };
        }

        async Task<PermissionStatus> CheckAndRequestPermissionAsync(BasePermission permission)
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;



        }
    }
}



