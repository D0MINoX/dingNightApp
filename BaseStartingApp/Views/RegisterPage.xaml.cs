using BaseStartingApp.Models;
using BaseStartingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseStartingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
            Button myButton = new Button
            {
                Text = "Zaloguj się",
                Command = new Command<NavigationData>(AppNavigation.NavigateTo),
                CommandParameter = new NavigationData { location = "LoginPage" }
            };

            layout.Children.Add(myButton);
        }

        private async void usernameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            //await usernameValidation();
            ValidationData data = await BaseStartingApp.ViewModels.UserInterface.usernameValidation(usernameEntry.Text);

            if (data.correct == false)
                usernameErrorLabel.Text = data.errorInfo;
            else
                usernameErrorLabel.Text = String.Empty;
        }

        private async void passwordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await BaseStartingApp.ViewModels.UserInterface.passwordValidation(passwordEntry.Text);

            if (data.correct == false)
                passwordErrorLabel.Text = data.errorInfo;
            else
                passwordErrorLabel.Text = String.Empty;
        }

        private async void confirmPasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            //await passwordValidation();
            ValidationData data = await BaseStartingApp.ViewModels.UserInterface.passwordConfirmValidation(passwordEntry.Text,confirmPasswordEntry.Text);
            if (data.correct == false)
                confirmPasswordErrorLabel.Text = data.errorInfo;
            else
                confirmPasswordErrorLabel.Text = String.Empty;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await UserInterface.RegisterUser(usernameEntry.Text, passwordEntry.Text, confirmPasswordEntry.Text);
        }

    }
}