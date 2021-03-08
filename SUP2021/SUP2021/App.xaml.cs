using SUP2021.Services;
using SUP2021.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
