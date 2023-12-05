using BaseStartingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStartingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseStartingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

            Button myButton = new Button
            {
                Text = "Utwórz konto",
                Command = new Command<NavigationData>(AppNavigation.NavigateTo),
                CommandParameter = new NavigationData { location = "RegisterPage" }
            };

            layout.Children.Add(myButton);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            await ViewModels.UserInterface.LogInUserValidation(username,password);
        }
    }
}