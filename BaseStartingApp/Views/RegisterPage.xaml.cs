﻿using BaseStartingApp.Models;
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
            Color butcolor = Color.FromRgb(0, 204, 153);
            Color tekstkolor = Color.White;
            Button myButton = new Button
            {
                Text = "Zaloguj się",
                Padding = 5,
                BackgroundColor = butcolor,
                TextColor = tekstkolor,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = 10,
                Command = new Command<NavigationData>(AppNavigation.NavigateTo),
                CommandParameter = new NavigationData { location = "LoginPage" }
                
            };

         gridrej.Children.Add(myButton,0,7);
        }

        private async void emailEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ValidationData data = await UserInterface.emailValidation(emailEntry.Text);

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
            ValidationData data = await BaseStartingApp.ViewModels.UserInterface.passwordConfirmValidation(passwordEntry.Text,confirmPasswordEntry.Text);
            if (data.correct == false)
                confirmPasswordErrorLabel.Text = data.errorInfo;
            else
                confirmPasswordErrorLabel.Text = String.Empty;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await UserInterface.RegisterUser(
                emailEntry.Text, 
                nameEntry.Text, 
                surnameEntry.Text, 
                phoneEntry.Text, 
                passwordEntry.Text, 
                confirmPasswordEntry.Text
                );


        }


    }
}