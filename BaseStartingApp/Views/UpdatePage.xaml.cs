using BaseStartingApp.Models;
using BaseStartingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;

namespace BaseStartingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePage : ContentPage
    {
        public UpdatePage()
        {
            InitializeComponent();
            emailEntry.Text = App.LoggedUser.email;
            nameEntry.Text = App.LoggedUser.name;
            surnameEntry.Text = App.LoggedUser.surname;
            phoneEntry.Text = App.LoggedUser.phone;
        }

        private async void emailEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await UserInterface.emailUpdateValidation(emailEntry.Text);

            if (data.correct == false)
                emailErrorLabel.Text = data.errorInfo;
            else
                emailErrorLabel.Text = String.Empty;
        }

        private async void phoneEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await UserInterface.phoneValidation(phoneEntry.Text);

            if (data.correct == false)
                phoneErrorLabel.Text = data.errorInfo;
            else
                phoneErrorLabel.Text = String.Empty;
        }

        private async void nameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await UserInterface.nameValidation(nameEntry.Text);

            if (data.correct == false)
                nameErrorLabel.Text = data.errorInfo;
            else
                nameErrorLabel.Text = String.Empty;
        }

        private async void surnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await UserInterface.surnameValidation(surnameEntry.Text);

            if (data.correct == false)
                surnameErrorLabel.Text = data.errorInfo;
            else
                surnameErrorLabel.Text = String.Empty;
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
            ValidationData data = await BaseStartingApp.ViewModels.UserInterface.passwordConfirmValidation(passwordEntry.Text, confirmPasswordEntry.Text);
            if (data.correct == false)
                confirmPasswordErrorLabel.Text = data.errorInfo;
            else
                confirmPasswordErrorLabel.Text = String.Empty;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string hashedPassword = null;
            string confirmHashedPassword = null;
            if (!String.IsNullOrEmpty(passwordEntry.Text))
            {
                hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwordEntry.Text)));
                confirmHashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwordEntry.Text)));
            }
            
            bool result = await UserInterface.UpdateUser(
                emailEntry.Text,
                nameEntry.Text,
                surnameEntry.Text,
                phoneEntry.Text,
               (String.IsNullOrEmpty(passwordEntry.Text) ? App.LoggedUser.password : hashedPassword),
               (String.IsNullOrEmpty(confirmPasswordEntry.Text) ? App.LoggedUser.password : confirmHashedPassword) 
                );

            /*if (result)
            {
                emailEntry.Text = App.LoggedUser.email;
                nameEntry.Text = App.LoggedUser.name;
                surnameEntry.Text = App.LoggedUser.surname;
                phoneEntry.Text = App.LoggedUser.phone;
            }*/

        }
    }
}