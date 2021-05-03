using System;
using SUP2021.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;
using System.ComponentModel;
using SUP2021.Views;

namespace SUP2021.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public ICommand OnSelectPermissionChangeCommand { get; set; }
        public ICommand GoToSettingsCommand { get; set; }
        public ICommand LoadPermissionCommand { get; set; }

        public ICommand GoToEditProfile { get; set; }


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

            GoToSettingsCommand = new Command(() =>
           {
               Xamarin.Essentials.AppInfo.ShowSettingsUI();
           });

            
    }


        //public bool SwitchMe
        //{
        //    get => Preferences.Get(nameof(SwitchMe), false);
        //    set
        //    {

        //        Preferences.Set(nameof(SwitchMe), value);
        //        OnPropertyChanged(nameof(SwitchMe));
        //    }


        //}

        async Task LoadPermissions()
        {
            PermissionsList = new List<Settings>()
            {
                { await CreatePermission(new Camera(), "ic_cam", "Camera", "Access to media and camera")},
                { await CreatePermission(new Microphone(), "ic_mic", "Mic", "Voice activation") },
                { await CreatePermission(new LocationWhenInUse(), "ic_location", "Location", " Access aproximate location (network) and precise location (GPS)") },
                { await CreatePermission(new StorageRead(), "ic_storage", "Storage", "⚠ This app requires access to your storage, if the permission is revoked some functions will stop working!") }

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



