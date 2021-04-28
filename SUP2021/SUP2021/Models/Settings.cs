using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SUP2021.Models
{
   public  class Settings : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
        public Xamarin.Essentials.Permissions.BasePermission Permission { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
