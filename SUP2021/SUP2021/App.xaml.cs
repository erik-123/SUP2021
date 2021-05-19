using Shiny;
using Shiny.Notifications;
using SQLite;
using SUP2021.Data;
using SUP2021.Services;
using SUP2021.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace SUP2021
{
    public partial class App : Application
    {
        static NoteDatabase database;
        private static TestDatabase testDatabase;
        public static string FilePath;

        public TestDatabase TestDatabase
        {
            get
            {
                if (testDatabase == null) {

                    testDatabase = new TestDatabase();
                
                }
                return TestDatabase;
            
            }
        }
        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                    //String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    //string dbFile = Path.Combine(path, "myDbSQLite.db3");
                    //var s = new SQLiteConnection(dbFile, SQLiteOpenFlags.Create);
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

        public App(string filePath)
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            FilePath = filePath;



        }

        protected override async void OnStart()
        {
            await SendNotificationNow();
            await ScheduleLocalNotification(DateTimeOffset.UtcNow.AddMinutes(1));
        }

        Task SendNotificationNow()
        {
            var notification = new Notification
            {
                Title = "Kom ihåg att titta till våra nya produkter",
                Message = "",
            };

            return ShinyHost.Resolve<INotificationManager>().RequestAccessAndSend(notification);
        }

        Task ScheduleLocalNotification(DateTimeOffset scheduledTime)
        {
            var notification = new Notification
            {
                Title = "Alltid bra priser och erbjudanden",
                Message = "",
                ScheduleDate = scheduledTime
            };

            return ShinyHost.Resolve<INotificationManager>().Send(notification);
        }
    



/*protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }*/
    }
}
