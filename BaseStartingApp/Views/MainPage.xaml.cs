using BaseStartingApp.Models;
using BaseStartingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseStartingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_TappedRegister(object sender, EventArgs e)
        {
            AppNavigation.NavigateTo(new NavigationData { location = "RegisterPage" });
        }

        private void TapGestureRecognizer_TappedLogin(object sender, EventArgs e)
        {
            AppNavigation.NavigateTo(new NavigationData { location = "LoginPage" });
        }
    }
}
