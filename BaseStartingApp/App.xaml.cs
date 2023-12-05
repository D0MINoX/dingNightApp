using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BaseStartingApp.Views;
using BaseStartingApp.ViewModels;
using BaseStartingApp.Models;
using System.IO;

namespace BaseStartingApp
{
    public partial class App : Application
    {
        private static Database database;
        private static Users loggedUser;
        public static Database Database
        {
            //nawiązanie połączenia z bazą danych lub stworzenie nowej jeżeli ta nieistnieje w podanym miejscu
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(
                              Environment.SpecialFolder.LocalApplicationData), "database.db3"));

                    Task.Run(async () => { await database.InitializeAsync(); }).Wait();
                }

                return database;
            }
        }

        public static Users LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
