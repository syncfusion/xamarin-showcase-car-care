using System;
using System.Diagnostics;
using System.IO;
using CarCare;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBaseService))]
[assembly: Dependency(typeof(NavigationService))]
namespace CarCare
{
    public partial class App : Application
    {
        private static SQLiteAsyncConnection dbConnection;
        internal static NavigationService NavigationService;
        internal static DataBaseService DataBaseService;

        public static SQLiteAsyncConnection DatabaseConnection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = new SQLiteAsyncConnection(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CarCareDB.db3"))
                    {
                        Trace = true,
                        Tracer = new Action<string>(q => Debug.WriteLine(q))
                    };
                }

                return dbConnection;
            }
        }

        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

            NavigationService = new NavigationService();
            DataBaseService = new DataBaseService();

            bool hasUserLogged = Preferences.Get("hasUserLogged", false);
            if (hasUserLogged)
            {
                NavigationService.NavigateToAsync<ProjectPageViewModel>();
            }
            else
            {
                NavigationService.NavigateToAsync<LoginPageViewModel>();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}