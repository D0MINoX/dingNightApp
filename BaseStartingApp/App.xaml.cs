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
        private static Users loggedUser;

        public static Users LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
