using SUP2021.Data;
using SUP2021.Services;
using SUP2021.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SUP2021
{
    public partial class App : Application
    {
        static NoteDatabase database;

        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }
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
